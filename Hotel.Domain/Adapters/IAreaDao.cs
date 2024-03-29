﻿using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities;

namespace Hotel.Domain.Adapters;

public interface IAreaDao : IAddDao<Area>, IModifyDao<Area>, IGetDao<Area>
{
}