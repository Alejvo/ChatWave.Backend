﻿using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Groups;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Update
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, ErrorOr<Unit>>
    {
        private readonly IGroupRepository _repository;

        public UpdateGroupCommandHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            byte[] imageBytes = null;
            if (request.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.Image.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }
            }
            var group = await _repository.GetById(GroupProcedures.GetGroupById,new { request.Id });
            if(group != null)
            {
                var updatedGroup = new
                {
                    request.Id,
                    request.Name,
                    request.Description,
                    imageBytes
                };
                await _repository.UpdateAsync(GroupProcedures.UpdateGroup, updatedGroup);
            }

            return Unit.Value;
        }
    }
}
