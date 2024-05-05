using Application.Repositories.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Comment
{
    public class CommentWrapper : ICommentWrapper
    {
        private readonly ICommentDA _commentDA;

        public CommentWrapper(ICommentDA commentDA)
        {
            _commentDA = commentDA;
        }
        public void CreateData(Core.Dto.PMDb.Comment model)
        {
            _commentDA.CreateData(model);
        }
        public void Delete(Core.Dto.PMDb.Comment model)
        {
            _commentDA.Delete(model);
        }
        public IEnumerable<Core.Dto.PMDb.Comment> GetAllData()
        {
            return _commentDA.GetAllData();
        }
        public Core.Dto.PMDb.Comment GetDataById(int id)
        {
            return _commentDA.GetDataById(id);
        }
        public void Update(Core.Dto.PMDb.Comment model)
        {
            _commentDA.Update(model);
        }
    }
}
