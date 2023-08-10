# Event Sourcing Pattern

<p align="center">
  <img width="450" height="150" src="https://github.com/alierguc1/SagaMicroservices/blob/EventSourcingPattern/EventSourcing.API/raw/evenstore_logo.png?raw=true">
  <br/>
  <img width="200" height="200" src="https://github.com/alierguc1/SagaMicroservices/blob/SagaChoreographyPattern/raw/postgresql.png?raw=true">
</p>

## - Proje Hakkında
Bu Proje'de kullanılan pattern Event Sourcing'dir. EventStore sistemi ile datalar kaydedilir. Ayrıca PostgreSql kullanılmıştır. Yapısal olarak CQRS Pattern ile mükemmel
bir uyum içinde olduğundan ötürü CQRS ile birlikte eventler kullanılmıştır.
<hr/>
The pattern used in this Project is Event Sourcing. Take the data with the EventStore system. Also PostgreSql is used. Structurally excellent with CQRS Pattern
Events are used with CQRS because they are in harmony.<p align="center">
<br/>
  
- Yapısal modeli aşağıdaki gibidir.

- The structural model is as follows.

  <img width="900" height="450" src="https://github.com/alierguc1/SagaMicroservices/blob/EventSourcingPattern/EventSourcing.API/raw/diagrams.png?raw=true">
</p>

<hr/>
<br/>
- EventStore.
<br/>
<img width="900" height="450" src="https://github.com/alierguc1/SagaMicroservices/blob/EventSourcingPattern/EventSourcing.API/raw/eventstore_1.PNG?raw=true">
<img width="900" height="450" src="https://github.com/alierguc1/SagaMicroservices/blob/EventSourcingPattern/EventSourcing.API/raw/eventstore_2.PNG?raw=true">

<br/>
- PostgreSql.
<br/>
<img width="900" height="450" src="https://github.com/alierguc1/SagaMicroservices/blob/EventSourcingPattern/EventSourcing.API/raw/postgresql.PNG?raw=true">

