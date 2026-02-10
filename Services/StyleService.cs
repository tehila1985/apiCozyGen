using AutoMapper;
using Dto;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StyleService : IStyleService
    {
        IStyleRepository _r;
        IMapper _mapper;
        public StyleService(IStyleRepository i, IMapper mapperr)
        {
            _r = i;
            _mapper = mapperr;
        }
        public async Task<IEnumerable<DtoSyle_id_name>> GetStyles()
        {
            var u = await _r.GetStyles();
            var r = _mapper.Map<List<Style>, List<DtoSyle_id_name>>(u);
            return r;
        }
    }
}
