﻿using System;
using System.Collections.Generic;

namespace NorthwindDb;

public partial class UserOperationClaim
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OperationClaimId { get; set; }
}
