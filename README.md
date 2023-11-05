 # sys-matriculas

### O Problema:

Constantemente, alunos ficam em dúvida sobre quais disciplinas podem e/ou devem cursar, 
devido ao seu andamento no curso e os pré e co-requisitos das mesmas. 

### O Projeto:

Esse projeto tem o objetivo de ajudar um aluno a visualizar as possibilidades de obtenção de disciplinas, 
sera possivel visualizar as disciplinas já cursadas, e entender quais caminhos possíveis a partir deste estado.

### O Software:

Deve permitir cadastro de cursos, currículos e disciplinas, bem como o cadastro dos alunos e acompanhamento
das disciplinas cursadas pelos mesmos, e demonstrar de forma gráfica, as dependências existentes entre as disciplinas:
O sistema deve conter dois papeis de usuários, Coordenador e Aluno

O que foi desenvolvido para o Coordenador:
- CRUD dos cursos
- CRUD dos currículos
- CRUD das disciplinas ao criar ou editar disciplinas pode ser definido suas relações de pré e co-requisito
- CRUD de alunos

O que foi desenvolvido para o Aluno:
- Cada aluno pode consultar somente suas informações
- Manter o registro das disciplinas já cursadas pelo aluno ou seja o mesmo pode marcar disciplinas conluidas e informar suas notas, 
isso podera ser feito em uma visão do progresso de um Curso, essa pagina contem um detalhamento grafico das dependências existentes entre as disciplinas
