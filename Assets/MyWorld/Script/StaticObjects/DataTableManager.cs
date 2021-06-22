using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTableManager : MonoBehaviour
{
    public SInteractorTable InteractorTable => interactorTable;
    public SMentTable MentTable => mentTable;


    [SerializeField] private SInteractorTable interactorTable = null;
    [SerializeField] private SMentTable mentTable = null;

}
