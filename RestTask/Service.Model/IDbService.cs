using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Model
{
    public interface IDbService
    {
        Task<List<StudentEntity>> GetAll();
        Task<StudentEntity> Get(int id);
        Task<StudentEntity> Add(UpdateStudentRequest entity);
        Task<StudentEntity> Update(int id, UpdateStudentRequest entity);
        Task Delete(int id);
    }
}
