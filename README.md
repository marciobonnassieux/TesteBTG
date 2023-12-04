# TesteBTG

Aplicação POC, com um background worker de multiplas threads, publicando mensagens em uma fila AWS SQS de tempos em tempos.
Outru backgrownd worker atuando como consumidor da fila de mensagens, lendo-as e gravando-as em um banco AWS DynamoDB. Após a gravação bem sucedida, a mensagem é excluída da fila.

Vale mencionar que existem vários pontos de melhoria, tanto na organização, quanto na segurança e funcionalidade.
