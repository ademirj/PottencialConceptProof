using Pottencial.Domain.Entities;
using Pottencial.Domain.Interface;
using Pottencial.Infrastructure.CrossCutting.DependecyInjection;
using Pottencial.Tests.Domain.Entities.Fixtures;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace Pottencial.Tests.Behavior.QuotingInsurancePhysicalPerson
{
    [Binding]
    [Collection(nameof(CostumerPhysicalPersonCollection))]
    public class CotarSeguroSteps
    {
        private readonly CostumerPhysicalPersonFixture _costumerPhysicalPersonFixture;
        private ScenarioContext _scenarioContext;

        public CotarSeguroSteps(ScenarioContext scenarioContext, 
            CostumerPhysicalPersonFixture costumerPhysicalPersonFixture)
        {
            _scenarioContext = scenarioContext;
            _costumerPhysicalPersonFixture = costumerPhysicalPersonFixture;
        }

        [Given(@"que estou solicitando uma cotação de seguros para um cliente do tipo pessoa física")]
        public void DadoQueEstouSolicitandoUmaCotacaoDeSegurosParaUmClienteDoTipoPessoaFisica()
        {
            _scenarioContext["costumerPhysicalPerson"] = _costumerPhysicalPersonFixture.CreateValidCostumerPhysicalPerson();
            _scenarioContext["quoteService"] = (IQuoteService)DependencyInjectionManagement.GetServiceProvider().GetService(typeof(IQuoteService)); 
        }
        
        [Given(@"sua renda mensal é de '(.*)'")]
        public void DadoSuaRendaMensalEDe(Decimal incomeAmount)
        {
            var costumerPhysicalPerson = (CostumerPhysicalPerson)_scenarioContext["costumerPhysicalPerson"];
            costumerPhysicalPerson.SetIncomeAmount(incomeAmount);

            _scenarioContext["costumerPhysicalPerson"] = costumerPhysicalPerson;
        }
        
        [Given(@"o valor minimo permitido é de '(.*)'")]
        public void DadoOValorMinimoPermitidoEDe(Decimal minValueToQuoteInsurance)
        {
            _scenarioContext["minValueToQuoteInsurance"] = minValueToQuoteInsurance;
        }
        
        [When(@"realizar uma solicitação da cotação de seguro")]
        public void QuandoRealizarUmaSolicitacaoDaCotacaoDeSeguro()
        {
            try
            {
                var quoteService = (IQuoteService)_scenarioContext["quoteService"];
                quoteService.QuotingInsurancePhysicalPerson((CostumerPhysicalPerson)_scenarioContext["costumerPhysicalPerson"]);
            }
            catch(Exception error)
            {
                _scenarioContext["errorQuotingInsurancePhysicalPerson"] = error;
            }
        }
        
        [Then(@"uma mensagem de negação '(.*)' será apresentada")]
        public void EntaoUmaMensagemDeNegacaoSeraApresentada(string message)
        {
            var exception = _scenarioContext["errorQuotingInsurancePhysicalPerson"];
            var quoteService = (IQuoteService)_scenarioContext["quoteService"];

            Assert.NotNull(exception);
            Assert.NotNull(exception as Exception);
            Assert.Equal(quoteService.GetMinIncomeAmountToQuotingInsuranceLegalPerson(), (decimal)_scenarioContext["minValueToQuoteInsurance"]);
            Assert.Equal(((Exception)exception).Message, message);
        }

        [Then(@"nenhuma excessão será gerada")]
        public void EntaoNenhumaExcessaoSeraGerada()
        {
            Assert.Equal(_scenarioContext.ContainsKey("errorQuotingInsurancePhysicalPerson"), false);
        }
    }
}
