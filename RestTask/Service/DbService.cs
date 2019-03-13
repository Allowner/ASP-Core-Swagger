using AutoMapper;
using DAL.Context;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class DbService : IDbService
    {
        readonly ClassContext _context;
        readonly IMapper _mapper;
        private IMemoryCache cache;

        public DbService(ClassContext context, IMemoryCache memoryCache, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            cache = memoryCache;
        }

        public async Task<List<StudentEntity>> GetAll()
        {
            var dbStudents = await _context.Students.OrderBy(h => h.Id).ToArrayAsync();
            var students = dbStudents.Select(h => _mapper.Map<StudentEntity>(h)).ToList();

            return students;
        }

        public async Task<StudentEntity> Get(int id)
        {
            Student student = null;
            if (!cache.TryGetValue(id, out student))
            {
                student = await _context.Students.FirstOrDefaultAsync(p => p.Id == id);
                if (student != null)
                {
                    cache.Set(student.Id, student,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return _mapper.Map<Student, StudentEntity>(student);
        }

        public async Task<StudentEntity> Add(UpdateStudentRequest entity)
        {
            var dbStudent = _mapper.Map<UpdateStudentRequest, Student>(entity);
            _context.Students.Add(dbStudent);

            int number = await _context.SaveChangesAsync();
            StudentEntity student = _mapper.Map<StudentEntity>(dbStudent);
            if (number > 0)
            {
                cache.Set(student.Id, student, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

            return student;
        }

        public async Task<StudentEntity> Update(int id, UpdateStudentRequest entity)
        {
            var dbStudent = await _context.Students.FirstOrDefaultAsync(p => p.Id == id);
            if (dbStudent == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            _mapper.Map(entity, dbStudent);

            await _context.SaveChangesAsync();

            return _mapper.Map<StudentEntity>(dbStudent);
        }

        public async Task Delete(int id)
        {
            var dbStudent = await _context.Students.FirstOrDefaultAsync(p => p.Id == id);
            if (dbStudent == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            _context.Students.Remove(dbStudent);
            await _context.SaveChangesAsync();
        }
    }
}
