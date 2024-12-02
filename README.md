# pizzariaCRUDd
Um aplicativo CRUD simulando o funcionamento de um sistema de pedidos simples de uma pizzaria.

## Instruções, leia atentamente:
- Baixe o arquivo ZIP do repositório e extraia no diretório de sua preferência;
- Certifique-se de ter o MySQL instalado corretamente em seu computador;
- Rode o script que está na pasta base do projeto
  - ### CUIDADO:
    o script deletará qualquer esquema que tenha o nome ```pizzaria```. Caso não queira que isso aconteça, mude o nome do schema no script em algum editor de texto;
- Configure o arquivo de variáveis de ambiente ```db.env``` que se encontra dentro da pasta raiz do diretório. Valores default já estão lá, caso queira alterar, lembre-se de manter o mesmo nome do schema do script;
- Abra o arquivo ```pizzariaCRUD.sln``` dentro da pasta raiz com o aplicativo Visual Studio;
- Execute com o botão ```Iniciar```.

## Utilização do aplicativo
- AVISO: Foi observado um problema com Caixas de Mensagens do próprio Windows Forms, no qual elas podem demorar alguns segundos para carregar corretamente. O problema está relacionado à biblioteca, e não a essa solução.
- São exibidas as 3 tabelas do banco de dados, você pode interagir com cada uma delas por meio dos botões e dos inputs de texto;
- Certifique-se de não ultrapassar os limites de caracteres dos inputs para prevenir exceções;
- É possível criar e atualizar clientes e pizzas com base nos dados inseridos nos inputs;
- É possível deletar clientes e pizzas por meio de uma janela que aparece após clicar no botão;
- É possível criar e deletar pedidos por meio das janelas popups;
- Pedidos são deletados automaticamente quando uma pizza ou um cliente relacionados a ele são deletados.

## Detalhes sobre a implementação (observada na pasta ```pizzariaCRUD```)
![banco_de_dados](https://github.com/jpdaher/pizzariaCRUDd/blob/main/banco_de_dados.png)
- Acima está uma foto do banco de dados gerado pelo script;
- O arquivo ```Connection.cs``` é um auxiliar de conexão com o MySQL, lá estão definidas funções para queries e queries sem retorno;
- Foi utilizado o padrão de projeto Singleton para garantir que só haja uma instância de conexão;
  - Observa-se o uso do Singleton também na linha 21 do arquivo ```Form1.cs```.
- O arquivo ```Form1.cs``` contém as funções executadas ao clicar nos botões, além das queries realizadas e funções auxiliares, criadas para garantir a modularidade do código.
