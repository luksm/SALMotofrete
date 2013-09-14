using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALMvc.Models
{
    public class gMapsGeoCode
    {
        public Results results;
    }

    public class Results
    {
        public Address_components[] address_components;
        public String formatted_address;
        public Geometry geometry;
    }

    public class Address_components
    {
        public String long_name;
        public String short_name;
        public String[] types;
    }

    public class Geometry
    {
        public Location location;
    }

    public class Location
    {
        public String lat;
        public String lng;
    }
}