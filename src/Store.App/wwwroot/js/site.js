function AjaxModal() {
    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            bindClick();
        });

        function bindForm(dialog) {
            $('form', dialog).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#myModal').modal('hide');
                            $('#AddressTarget').load(result.url, function () {
                                bindClick();
                            });
                        } else {
                            $('#myModalContent').html(result);
                            bindForm();
                        }
                    }
                });
                return false;
            });
        }

        function bindClick() {
            $("a[data-modal]").click(function (e) {
                $('#myModalContent').load(this.href, function () {
                    $('#myModal').modal({ keyword: true }, 'show');
                    bindForm(this);
                });
                return false;
            });
        }
    });
}

function GetCEP() {
    $(document).ready(function () {

        function clearFormCEP() {
            $("#Address_PublicPlace").val("");
            $("#Address_District").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        function setReadOnly(readonly) {
            $('#Address_PublicPlace').attr('readonly', readonly);
            $('#Address_District').attr('readonly', readonly);
            $('#Address_City').attr('readonly', readonly);
            $('#Address_State').attr('readonly', readonly);
        }

        $("#Address_CEP").blur(function () {
            var cep = $(this).val().replace(/\D/g, '');

            if (cep != "") {
                var validateCEP = /^[0-9]{8}$/;

                if (validateCEP.test(cep)) {
                    setReadOnly(true);
                    $("#Address_PublicPlace").val("...");
                    $("#Address_District").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (data) {
                        if (!("erro" in data)) {
                            $("#Address_PublicPlace").val(data.logradouro);
                            $("#Address_District").val(data.bairro);
                            $("#Address_City").val(data.localidade);
                            $("#Address_State").val(data.uf);
                            setReadOnly(false);
                        } else {
                            clearFormCEP();
                            setReadOnly(false);
                            alert("CEP não encontrado.");
                        }
                    });
                } else {
                    clearFormCEP();
                    alert("CEP com formato inválido.");
                }
            } else {
                clearFormCEP();
            }
        });
    });
}

$(document).ready(function () {
    $("messageBox").fadeOut(2500);
})