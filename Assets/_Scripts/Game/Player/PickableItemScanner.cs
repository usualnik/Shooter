using System;
using UnityEngine;

public class PickableItemScanner : MonoBehaviour
{
    [Header("Scan Settings")]
    [SerializeField] private float _scanRadius = 3f;
    [SerializeField] private float _scanDelay = 0.1f;
    [SerializeField] private float _scanInterval = 0.5f;
    [SerializeField] private LayerMask _scanLayerMask;

    //private Inventory _inventory;

    public event EventHandler<OnItemFoundEventArgs> OnItemFound;
    public class OnItemFoundEventArgs : EventArgs
    {
        public BaseItemSO BaseItemFound;
    }

    private void Start()
    {
        InvokeRepeating(nameof(PerformScan), _scanDelay, _scanInterval);
    }

    private void PerformScan()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            _scanRadius,
            _scanLayerMask
        );

        foreach (var hit in hits)
        {
            var pickable = hit.GetComponent<IPickable>();
            
            if (pickable != null)
            {
                OnItemFoundEventArgs onItemFoundEventArgs = new OnItemFoundEventArgs()
                    {BaseItemFound = pickable.PickItem()};
                OnItemFound?.Invoke(this, onItemFoundEventArgs);
                break; 
            }
        }
    }

}