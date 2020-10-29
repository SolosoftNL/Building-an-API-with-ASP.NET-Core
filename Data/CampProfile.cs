using AutoMapper;
using CoreCodeCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
	public class CampProfile : Profile
	{
		public CampProfile()
		{
			this.CreateMap<Speaker, SpeakerModel>();
			this.CreateMap<Talk, TalkModel>();
			this.CreateMap<Camp, CampModel>()
				.ForMember(c => c.Venue, o => o.MapFrom(m => m.Location))
				.ForMember(c => c.Address1, o => o.MapFrom(m => m.Location.Address1))
				.ForMember(c => c.Address2, o => o.MapFrom(m => m.Location.Address2))
				.ForMember(c => c.Address3, o => o.MapFrom(m => m.Location.Address3))
				.ForMember(c => c.CityTown, o => o.MapFrom(m => m.Location.CityTown))
				.ForMember(c => c.StateProvince, o => o.MapFrom(m => m.Location.StateProvince))
				.ForMember(c => c.PostalCode, o => o.MapFrom(m => m.Location.PostalCode))
				.ForMember(c => c.Country, o => o.MapFrom(m => m.Location.Country))
				//.ForMember(c=>c.Talks, o=>o.MapFrom(m=>m.Talks))
			
				;
        //   this.CreateMap<Camp, TalkModel>().ForMember(c=> c.Abstract, o=> o.ConvertUsing<CampModel>()									
		}
	}
}
