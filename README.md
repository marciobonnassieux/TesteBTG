# TesteBTG

Aplicação POC, com um background worker de multiplas threads, publicando mensagens em uma fila AWS SQS de tempos em tempos.
Outru backgrownd worker atuando como consumidor da fila de mensagens, lendo-as e gravando-as em um banco AWS DynamoDB. Após a gravação bem sucedida, a mensagem é excluída da fila.

Vale mencionar que existem vários pontos de melhoria, tanto na organização, quanto na segurança e funcionalidade.

No momento, a aplicação está publicando mensagens aleatórias pelo publisher, e consumindo e gravando gravando em DB pelo consummer.
O publisher, inicia 5 threads, cada uma mandando uma mensagem.

Pretendo mudar o projeto para que se torne um leitor em tempo real de alguma api de valor de cripto moedas, ações etc, que tenham seus dados mudados de maneira constante e frequente, para se ter um histórico de alterações de valores.
Para tal, pretendo fazer uma WebAPI que registre qual dado deve ser monitorado à partir da escolha em uma lista suspensa.
Ao registrar, o publisher criará uma nova thread para monitorar apenas o dado escolhido para monitoramento, para então começar a registrar as mudanças na Fila SQS, para posterior leitura.

Quem sabe, futuramente, essa WEB API, cresça para um web app, com dashboards usando os dados coletados?
