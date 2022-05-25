# MIRNA WEB API PROJECT
[![SecurityCodeScan](https://github.com/FelipeKatao/Mirna/actions/workflows/securitycodescan.yml/badge.svg?branch=ApiMirnaWeb)](https://github.com/FelipeKatao/Mirna/actions/workflows/securitycodescan.yml)

[![CodeQL](https://github.com/FelipeKatao/Mirna/actions/workflows/codeql.yml/badge.svg)](https://github.com/FelipeKatao/Mirna/actions/workflows/codeql.yml)

## Como fazer o login com a Mirna API com Token de validação
Para fazer o login com a formiga é necessário rodar a token de validação com a nossa tecnologia  que utiliza sistema para que somente quem tenha a posse deste token consiga realizar os acessos no banco de dados, para fazer o login rode o comando: 

../Silvercon/connect/< Token >
Neste comando você consegue fazer a leitura do banco de dados setado pelo token, onde ele te redireciona e te mostra exatamente a resposta que você obteve com o seu data source 

../Silvercon/connect/< Token > / < TokenBanco > 
Com este comando você consegue fazer a validação em outro banco de dados a sua escolha lembrando que você não digita o nome do banco de dados você coloca a Token que é fornecida pela Mirna para fazer esse processo com mais segurança.

## Após conectado o que posso fazer? 
Ao passar o parâmetro ele executa uma única vez e depois expira o Token para poder logar e conseguir rodar outros comandos, você precisa configurar isso dentro da Mirna, onde você pode setar Querys para serem executadas quando esses bancos forem acessados, rodando esse comando te permitira fazer o que deseja: 

../Sivercon/< Token >/ <disp ou exdisp>
Esse comando permite que você abra o banco para entradas de querys ou feche completamente, sendo o disp para abertura e o exdisp para encerramento da conexão. 

../Mirna/connect/ < Token > / < TokenBanco > / < TokenQuery > /<parametros>
Após fazer a abertura do banco de dados rode as querys registradas na sua conta da Mirna, elas são construidas e caso necessite de algum parametro passe pelo ultimo comando.
Todos retornos desta etapa são feitos com Json permitindo o consumo pelo cliente, em qualquer linguagem de programação, iremos abordar isso mais a frente com exemplos prontos.

Ps: A conexão ao banco expira após uma tentativa então se frizer uma conexão já aberta ela se fecha automaticamente, assim evitando uso de multiplos acessos com o mesmo token.

## Suportes com outras tecnologias 
As tecnologias que oferecemos suporte até o momento são:
    
- MongoDb 
- XML (arquivo local)
- Firebase 
- Json 
Todos esses bancos de dados a mirna ira oferecer suporte, caso precise você pode utilizar diversos tipos
de conexão para a mesma conta Mirna. 
