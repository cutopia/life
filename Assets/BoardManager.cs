using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] int boardDim = 100;
    [SerializeField] float updateRate = 6.0f;
    float timer = 0.0f;
    private CellHandler[,] grid;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid(boardDim);
    }
    
    void InitializeGrid(int dim)
    {
        grid = new CellHandler[dim, dim];
        for (int row = 0; row < dim; row++)
        {
            for (int col = 0; col < dim; col++)
            {
                GameObject cell = Instantiate(cellPrefab) as GameObject;
                var pos = cell.transform.position;
                pos.x = col;
                pos.y = row;
                grid[col, row] = cell.GetComponent<CellHandler>();
                cell.transform.position = pos;
                cell.transform.SetParent(transform, false);
            }
        }
    }

    void Update()
    {
        // pause updates until after mouse interaction is done.
        if (Input.GetMouseButton(0))
        {
            timer = 0f;
            return;
        }
        timer += Time.deltaTime;
        if (timer > updateRate)
        {
            timer = 0f;
            UpdateGrid(boardDim);
        }
    }
    
    void UpdateGrid(int dim)
    {
        for (int row = 0; row < dim; row++)
        {
            for (int col = 0; col < dim; col++)
            {
                var c = GetNeighborCount(col, row);
                if (c > 0)
                {
                    Debug.Log("" + col + "," + row + ": " + c);
                }
                
                if (grid[col, row].State)
                {
                    grid[col, row].newState = c == 3 || c == 2;
                }
                else
                {
                    grid[col, row].newState = c == 3;
                }
            }
        }
        for (int row = 0; row < dim; row++)
        {
            for (int col = 0; col < dim; col++)
            {
                grid[col, row].State = grid[col, row].newState;
            }
        }
    }

    int GetNeighborCount(int pCol, int pRow)
    {
        int count = 0;
        for (int i = pCol - 1; i <= pCol + 1; i++)
        {
            for (int j = pRow - 1; j <= pRow + 1; j++)
            {
                if (i < 0 || j < 0)
                {
                    continue;
                }

                if (i >= boardDim || j >= boardDim)
                {
                    continue;
                }

                if (i == pCol && j == pRow)
                {
                    continue;
                }

                if (grid[i, j].State)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
