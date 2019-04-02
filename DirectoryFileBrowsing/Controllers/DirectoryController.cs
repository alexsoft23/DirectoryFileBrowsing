using DirectoryFileBrowsing.Domain.Entities;
using DirectoryFileBrowsing.Domain.IRepositories;
using DirectoryFileBrowsing.Infrastructure.Repositories;
using System.IO;
using System.Net;
using System.Web.Http;

namespace DirectoryFileBrowsing.Controllers
{
	public class DirectoryController : ApiController
	{


		public IDirectoryRepository _directoryRepository;

		public DirectoryController()
		{
			DirectoryRepository directoryRepository = new DirectoryRepository();
			_directoryRepository = directoryRepository;
		}

		public IHttpActionResult GetObList([FromUri] string path)
		{
			if (!Directory.Exists(path))
				return BadRequest("Not exist path");
			var dir = new DirectoryInfo(path);
			var parentPath = dir.Parent?.FullName ?? "";
			return Ok(new Page(parentPath, path, _directoryRepository.GetObList(path)));
		}

		public IHttpActionResult GetFolderInfo([FromUri] string paths)
		{
			if (Directory.Exists(paths) && paths != null)
				return Ok(_directoryRepository.GetFolderInfo(paths));
			return StatusCode(HttpStatusCode.BadRequest);
		}

		public IHttpActionResult GetDrives()
		{
			return Ok(new Page("none", "All Disk", _directoryRepository.GetDrives()));
		}
	}
}
