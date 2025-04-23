using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;

namespace BlazorAI_MCP.Web.Services
{
    public class GitHubMCPService
    {
        private readonly string[] _toolsToUse = { "get_issue", "search_users" };

        private readonly Task<IMcpClient> _clientTask = McpClientFactory.CreateAsync(
            new StdioClientTransport(new()
                {
                    Command = "npx",
                    Arguments = ["-y", "@modelcontextprotocol/server-github"],
                    Name = "GitHub",
                })
            );
        public async Task<IEnumerable<AIFunction>> GetToolsAsync()
        {
            var client = await _clientTask;
            var allTools = await client.ListToolsAsync();
            return allTools.Where(tool => _toolsToUse.Contains(tool.Name));
        }
    }

}
