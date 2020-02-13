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