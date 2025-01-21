namespace DownstreamCustomers.DB;

public record Branch
{
    public int StartNode { get; set; }
    public int EndNode { get; set; }
}
public record Customer
{
    public int Node { get; set; }
    public int NumberOfCustomers {  get; set; }
}
public record Network
{
    public required List<Branch> Branches { get; set; }
    public required List<Customer> Customers { get; set; }
}
public record Request
{
    public required Network Network { get; set; }
    public int SelectedNode {  get; set; }
}
public record Response
{
    public int DownstreamCustomers { get; set; }
}
public class DownstreamCustomersDB()
{
    public static Response GetDownCust(Request request)
    {
        int selectedNode = request.SelectedNode;
        Network network = request.Network;
        List<int> nodes = [];
        int i = 0;
        for (; i < network.Branches.Count; i++) // find the first branch with the selected node
        {
            if (network.Branches[i].StartNode == selectedNode) // add both start and end node to the list
            {
                nodes.Add(network.Branches[i].StartNode);
                nodes.Add(network.Branches[i].EndNode);
                break;
            }
        }
        if (nodes.Count == 0) // if node isnt the start of a branch then there is nothing "downstream" of it
        {
            return new Response {DownstreamCustomers = 0};   
        }
        else
        {
            for (int j = i + 1; j < network.Branches.Count; j++) // loop through rest of branches
            {
                for (int k = 0; k < nodes.Count; k++) // to check whether either node of the branch is in the nodes list
                {
                    if (network.Branches[j].StartNode == nodes[k]) // checks for the whether start node of the list is in the nodes and adds endnode if it is
                    {
                        nodes.Add(network.Branches[j].EndNode);
                    }
                }
            }
            int sum = 0;
            for (int k = 1; k < nodes.Count; k++) // ignoring selected node as it is not "downstream"
            {
                for (int l = 0; l < network.Customers.Count; l++)
                {
                    if (nodes[k] == network.Customers[l].Node)
                    {
                        sum += network.Customers[l].NumberOfCustomers;
                    }
                }
            }
            return new Response { DownstreamCustomers = sum};
        }
    }
}