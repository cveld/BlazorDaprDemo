# The promise of Distributed Application Runtime (Dapr)
Building microservices can be a challenge by a variety of reasons.
If you are looking for means to decouple your application concerns from infrastructure concerns, the Distributed Application Runtime (Dapr) can be a great fit.
Get started by visiting https://dapr.io/

At TEQnation 2022 I did a talk about building microservices with Dapr and .NET.
In this repo you will find the demo app code. 
The slidedeck can also be found in this repo:
[2022-05 TEQnation - Building Microservices with Dapr and .NET - Carl in 't Veld.pptx]

If you have any questions or feedback, don't hesitate to reach out to me through any channels or raise a ticket in this very repo.

# Architecture
The demo app is utilizing the following patterns:
* .NET Blazor front-end. With Blazor you can build real single page apps in the browser. Blazor provides two modes of operation: WebAssembly and websocket server. This demo app applies the latter - with a websocket the browser events are passed to the server. The server passes html diffs back to the browser.
* Dapr - it decouples infrastructure implementation from application code. The following building blocks are applied:
   * Service Invocation. Dapr helps you routing http traffic to the right service
   * State Management. Dapr helps you storing simple key value date into a store of choice
   * Pubsub. Dapr helps you wiring up a pubsub infrastructure of choice
   