﻿using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100Common.DTOs
{
    public class GetMonthDTO
    {
        public string Id { get; set; }
    }
    public class GetMonthListDTO : R_APIResultBaseDTO
    {
        public List<GetMonthDTO> Data { get; set; }
    }
}
