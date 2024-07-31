using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

/// <summary>
/// Interface for node connection checking.
/// </summary>
public interface INodeConnectionChecker
{
    bool IsConnected();
    void CheckConnections();
}


