<body>
  <h1 align = center>UPBank</h1>
  <br>
  <h2 align = center>Objetivo Geral</h2>
  <h3 align = center>Desenvolver uma aplicação que irá compartilhar diversas informações entre os Microserviços que controlará as operações 
    financeiras de um banco denominado UP. Os módulos são referentes aos controles de:</h3>
  <ul>
    <li>Clientes</li>
    <li>Endereço</li>
    <li>Agencia</li>
    <li>Contas</li>
    <li>Solicitações de abertura</li>
    <li>Realizar Operações Saque, Depósito, Consulta e Pagamentos</li>
  </ul>
  <h2 align = center>Cadastros Básicos</h2>
  <h3 align = center>Todos os módulos deverão ter o CRUD completo, salvo exceções definidas em documento, valendo-se assim esta definição. Todos 
    os campos de status servem para referenciar possíveis cadastros de restrição. As operações de CRUD serão:</h3>
  <ul>
    <li>Cadastrar (CREATE) (Todos os campos serão dados como necessários, salvo os que forem definidos como nullables).</li>
    <li>Localizar (READ) - um registro específico a ser localizado, preferencialmente por seu dado que o definem como único.</li>
    <li>Editar (UPDATE) - Alterar os dados de um registro, desde que este não seja uma informação única.</li>
    <li>Apagar (DELETE) – Remove o registro, movendo-o a uma outra área do banco para que, se necessário, possa ser recuperado.</li>
  </ul>
  <p>O Banco UP possui diversas agências. As agências também possuem caixas eletrônicos, através dos quais os clientes podem realizar algumas 
    transações: sacar dinheiro, depositar na conta, transferir valores para outra conta, consultar extrato e saldo, realizar pagamentos e 
    solicitar empréstimos.</p>
  <p>Para se cadastrar no banco, o potencial cliente deve dirigir-se a uma agência física e solicitar a abertura de uma conta. Há três opções de
    conta: universitária, normal e cliente vip. O funcionário irá avaliar, de acordo com o perfil do cliente e da sua faixa salarial, o tipo de 
    conta que lhe será atribuída. Posteriormente, o gerente da agência deve aprovar ou não a criação da conta.</p>
  <p>Ressalte-se que, todo cliente do banco possui uma conta corrente, uma conta poupança, um cartão de crédito, que pode ou não ser desbloqueado 
    para uso e um cheque especial (limite), que é um valor em dinheiro atribuído a sua conta e que pode ser consumido caso o 
    cliente deseje ou o saldo da sua conta se torne negativo. O valor do cheque especial é atribuído no momento da criação da conta, de acordo 
    com o tipo de conta e o perfil do cliente.</p> 
  <h2 align = center>Entidades</h2>
  <h3 align = center>Entidade Clientes</h3>
  <p>O banco atende apenas pessoas físicas, sendo assim, não teremos que nos preocupar em gerenciar pessoas jurídicas. Os clientes menores de 18 
    anos podem ter conta somente conjunta com algum maior de idade. Os registros de restrição devem ter apenas as funções de incluir e remover 
    o cadastro.</p>
  <h3 align = center>Entidade Agencia</h3>
  <p>Somente serão aceitos cadastros de pessoas jurídicas. Toda agência cadastrada deve ter pelo menos um gerente vinculado. Os registros de 
    restrição devem ter apenas as funções de incluir e remover o cadastro.</p>
  <h3 align = center>Entidade Conta</h3>
  <p>Toda conta deverá ser liberada pelo gerente da agência. Até isso acontecer, a conta existe porém é inativa, não podendo fazer nenhum tipo de 
    operação. Após aprovação, as contas podem ser bloqueadas novamente.</p>
</body>
