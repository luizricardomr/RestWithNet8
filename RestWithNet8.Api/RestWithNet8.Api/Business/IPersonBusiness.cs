using RestWithNet8.Api.Data.VO;

namespace RestWithNet8.Api.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        PersonVO Update(PersonVO person);
        void Delete(long id);
        List<PersonVO> FinAll();
    }
}
