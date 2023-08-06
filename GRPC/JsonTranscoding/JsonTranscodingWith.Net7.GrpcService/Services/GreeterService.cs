﻿using Grpc.Core;
using GrpcService;

namespace JsonTranscodingWith.Net7.GrpcService.Services;

public class GreeterService : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = $"Hello {request.Name}"
        });
}
}