﻿using System;
using System.Collections.Generic;
using Restfulie.Server.MediaTypes;
using System.Linq;

namespace Restfulie.Server.Negotiation
{
    public class AcceptHeaderToMediaType
    {
        private readonly IMediaTypeList mediaTypes;

        public AcceptHeaderToMediaType(IMediaTypeList mediaTypes)
        {
            this.mediaTypes = mediaTypes;
        }

        public IMediaType GetMediaType(string accept)
        {
            var types = accept.Split(',');

            var acceptedMediaType = new List<QualifiedMediaType>();

            foreach(var type in types)
            {
                if (type.Trim().Equals("*/*"))
                {
                    acceptedMediaType.Add(new QualifiedMediaType(mediaTypes.Default, 1));
                }
                else
                {
                    string format;
                    var qualifier = 1.0;

                    if (type.Contains(";"))
                    {
                        var typeInfo = type.Split(';');
                        format = typeInfo[0];
                        qualifier = Convert.ToDouble(typeInfo[1].Split('=')[1]);
                    }
                    else
                    {
                        format = type;
                    }

                    var mediaType = mediaTypes.Find(format);
                    if (mediaType != null) acceptedMediaType.Add(new QualifiedMediaType(mediaType, qualifier));
                }
            }

            if(acceptedMediaType.Count == 0) throw new RequestedMediaTypeNotSupportedException();
            return MostQualifiedMediaType(acceptedMediaType);
        }

        private IMediaType MostQualifiedMediaType(IEnumerable<QualifiedMediaType> acceptedMediaType)
        {
            var maxQualifier = acceptedMediaType.Max(m => m.Qualifier);
            return acceptedMediaType.Where(m => m.Qualifier == maxQualifier).First().MediaType;
        }

        class QualifiedMediaType
        {
            public IMediaType MediaType { get; private set; }
            public double Qualifier { get; private set; }

            public QualifiedMediaType(IMediaType mediaType, double qualifier)
            {
                MediaType = mediaType;
                Qualifier = qualifier;
            }
        }
    }
}