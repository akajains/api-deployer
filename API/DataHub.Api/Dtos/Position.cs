﻿namespace DataHub.Api.Dtos
{
    public class Position
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public PositionBasedOn BasedOn { get; set; }
    }
}
