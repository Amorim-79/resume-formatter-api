using FluentValidation;
using ResumeFormatter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeFormatter.Domain.Interfaces.Service
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
        bool Delete(int id);
        IList<TEntity> Get();
        TEntity GetById(int id);
        TEntity Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
    }
}
