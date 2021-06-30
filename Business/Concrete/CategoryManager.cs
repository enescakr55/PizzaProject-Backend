using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            this.categoryDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            categoryDal.Add(category);
            return new SuccessResult("Başarıyla Eklendi");
        }

        public IResult Delete(Category category)
        {
            categoryDal.Delete(category);
            return new SuccessResult("Başarıyla Silindi");
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(categoryDal.GetAll());
        }

        public IResult Update(Category category)
        {
            categoryDal.Update(category);
            return new SuccessResult("Başarıyla Güncellendi");
        }
    }
}
