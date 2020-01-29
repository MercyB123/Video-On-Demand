﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;

namespace VOD.Common.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Instructor, InstructorDTO>().ReverseMap();

            CreateMap<Course, DTOModels.Admin.CourseDTO>().ForMember(d => d.Instructor, a => a.MapFrom(c =>
                    c.Instructor.Name))
                .ReverseMap()
                .ForMember(d => d.Instructor, a => a.Ignore());

            CreateMap<Module, DTOModels.Admin.ModuleDTO>()
                .ForMember(d => d.Course, a => a.MapFrom(c =>
                    c.Course.Title))
                .ReverseMap()
                .ForMember(d => d.Course, a => a.Ignore())
                .ForMember(d => d.Downloads, a => a.Ignore())
                .ForMember(d => d.Videos, a => a.Ignore());
            CreateMap<Video, DTOModels.Admin.VideoDTO>()
                       .ForMember(d => d.Module, a => a.MapFrom(c =>
                           c.Module.Title))
                       .ForMember(d => d.Course, a => a.MapFrom(c =>
                           c.Course.Title))
                       .ReverseMap()
                       .ForMember(d => d.Module, a => a.Ignore())
                       .ForMember(d => d.Course, a => a.Ignore());

            CreateMap<Download, DTOModels.Admin.DownloadDTO>()
                .ForMember(d => d.Module, a => a.MapFrom(c =>
                    c.Module.Title))
                .ForMember(d => d.Course, a => a.MapFrom(c =>
                    c.Course.Title))
                .ReverseMap()
                .ForMember(d => d.Module, a => a.Ignore())
                .ForMember(d => d.Course, a => a.Ignore());
        }
    }

}
