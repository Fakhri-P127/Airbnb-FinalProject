﻿namespace Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedPropertyResponses
{
    public class HostInPropertyResponse
    {
        public Guid Id { get; set; }
        public AppUserInHost Host { get; set; }
    }
    public class AppUserInHost
    {
        public string Id { get; set; }
        //Firstname ve Lastname i birleshdir
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
    }
}