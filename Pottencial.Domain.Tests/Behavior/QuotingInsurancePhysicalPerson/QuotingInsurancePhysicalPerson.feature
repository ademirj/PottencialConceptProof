Funcionalidade: Dado que sou um consumidor de API
Gostaria de testar o comportamento da cotação de seguro para pessoas fisicas

Cenario: Avaliar se o cliente possui uma renda minima superior ao minimo permitido
	Dado que estou solicitando uma cotação de seguros para um cliente do tipo pessoa física
	E sua renda mensal é de '900,00'
	E o valor minimo permitido é de '3000,00'
	Quando realizar uma solicitação da cotação de seguro
	Entao uma mensagem de negação 'it is not possible to quote the insurance, as the minimum income required was not reached' será apresentada