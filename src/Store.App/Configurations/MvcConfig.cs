using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Store.App.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor '{0}' não é válido para {1}.");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Não foi fornecido um valor para o campo {0}.");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Campo obrigatório.");
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor '{0}' não é válido.");
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor fornecido é inválido.");
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser um número.");
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor fornecido é inválido para {0}.");
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "O valor fornecido é inválido para {0}.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo {0} deve ser um número.");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "O valor nulo é inválido.");

                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddDataAnnotationsLocalization().AddViewLocalization();

            return services;
        }
    }
}
