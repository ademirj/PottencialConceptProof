Funcionalidade: Cotar seguro
	PARA que eu possa cotar um seguro
	SENDO um cliente pessoa fisica 
	POSSO consultar a disponibilidade e o valor da cobertura


	Cenario: Avaliar se o cliente com uma renda superior a minima permitida, conseguirá realizar uma cotação de seguro
		Dado que estou solicitando uma cotação de seguros para um cliente do tipo pessoa física
		E sua renda mensal é de '3500,00'
		E o valor minimo permitido é de '3000,00'
		Quando realizar uma solicitação da cotação de seguro
		Entao nenhuma excessão será gerada

	Cenario: Avaliar se será negado a cotação de um seguro de um cliente sem caracteristicas válidas
		Dado que estou solicitando uma cotação de seguros para um cliente do tipo pessoa física
		E sua renda mensal é de '900,00'
		E o valor minimo permitido é de '3000,00'
		Quando realizar uma solicitação da cotação de seguro
		Entao uma mensagem de negação 'it is not possible to quote the insurance, as the minimum income required was not reached' será apresentada

