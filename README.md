# Saga Choreography Microservices

<p align="center">
  <img width="250" height="250" src="https://github.com/alierguc1/SagaMicroservices/blob/SagaChoreographyPattern/raw/masstransit.png?raw=true">
  <br/>
  <img width="200" height="200" src="https://github.com/alierguc1/SagaMicroservices/blob/SagaChoreographyPattern/raw/mssql.png?raw=true">
  <img width="200" height="200" src="https://github.com/alierguc1/SagaMicroservices/blob/SagaChoreographyPattern/raw/postgresql.png?raw=true">
  <img width="350" height="100" src="https://github.com/alierguc1/SagaMicroservices/blob/SagaChoreographyPattern/raw/redis.png?raw=true">
  <img width="350" height="75" src="https://github.com/alierguc1/SagaMicroservices/blob/SagaChoreographyPattern/raw/rabbitmq.png?raw=true">
</p>

## - Proje Hakkında
Bu proje'de 3 adet microservice bulunmaktadır. Servisler birbirleri ile MassTransit vasıtasıyla haberleşmektedir. Servisler birbirlerine event fırlatmakta olup, ACID Prensibinden taviz verilmeyerek oluşturulmuştur.
Projenin şeması aşağıdaki şekildedir;
<hr/>
There are 3 microservices in this project. The services communicate with each other through MassTransit. The services are throwing events at each other and have been created without compromising the ACID Principle. The scheme of the project is as follows;
<p align="center">
  <img width="900" height="900" src="https://github.com/alierguc1/SagaMicroservices/blob/SagaChoreographyPattern/raw/saga_schema.png?raw=true">
</p>



| Event veya Consumer Adı | Tip                | Açıklama                |
| :-------- | :------------------------- | :------------------------- |
| `OrderCreatedEvent` | Event | Sipariş oluşturulduğu gönderilen event. |
| `PaymentCompleted Event`| Event  | Ödeme işlemi tamamlandığında gönderilen event. |
| `PaymentFailEvent`| Event  | Ödeme işleminin başarısız olduğu durumlarda oluşturulan event. |
| `StockNotReservedEvent`| Event  | Stokta bulunmayan ürün olduğunda gönderilen event. |
| `OrderCreatedEventConsumer`| Consumer Event  | Sipariş oluşturulduğunda başarılı olanı consume eden event. |
| `PaymentFailedEventConsumer`| Consumer Event  | Ödeme işlemi başarısız olduğunda consume eden event. |
| `StockNotReservedEventConsumer`| Consumer Event  | Stokta ürün bulunmadığında consume edilen event. |
| `PaymentCompletedEventConsumer`| Consumer Event  | Ödeme işlemi başarılı olduğunda consume eden event. |

<br/>
<hr/>

| Event Or Consumer Name | Type                | Description                |
| :-------- | :------------------------- | :------------------------- |
| `OrderCreatedEvent` | Event | The event that was sent when the order was created. |
| `PaymentCompleted Event`| Event  | Event sent when payment is completed. |
| `PaymentFailEvent`| Event  | Event created when the payment process fails. |
| `StockNotReservedEvent`| Event  | Event sent when there is a product out of stock. |
| `OrderCreatedEventConsumer`| Consumer Event  | Event that consumes the successful one when the order is created. |
| `PaymentFailedEventConsumer`| Consumer Event  | The event that consumes when the payment process fails. |
| `StockNotReservedEventConsumer`| Consumer Event  | Event consumed when there is no product in stock. |
| `PaymentCompletedEventConsumer`| Consumer Event  | The event that consumes when the payment transaction is successful. |



