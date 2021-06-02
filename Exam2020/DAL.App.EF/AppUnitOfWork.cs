using System.Threading.Tasks;
using AutoMapper;
using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain.App;
using Domain.App.Identity;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }

        public IAnswerRepository Answer => GetRepository(() => new AnswerRepository(UowDbContext, Mapper));
        public IQuizRepository Quiz => GetRepository(() => new QuizRepository(UowDbContext, Mapper));
        public IQuestionRepository Question => GetRepository(() => new QuestionRepository(UowDbContext, Mapper));
        public IResultRepository Result => GetRepository(() => new ResultRepository(UowDbContext, Mapper));


    }
}