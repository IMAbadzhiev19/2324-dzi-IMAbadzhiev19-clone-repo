// <copyright file="ICurrentUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PMS.Shared.Contracts;

/// <summary>
/// An interface representing the current user.
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Gets the unique ID of the user.
    /// </summary>
    string UserId { get; }
}