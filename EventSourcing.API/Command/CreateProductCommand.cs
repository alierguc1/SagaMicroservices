﻿using EventSourcing.API.Dtos;
using MediatR;

namespace EventSourcing.API.Command
{
    public class CreateProductCommand : IRequest
    {
        public CreateProductDto CreateProductDto { get; set; }



    }
}
