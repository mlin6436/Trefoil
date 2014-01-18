using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Trefoil.Helper;

namespace Trefoil.Library
{
    [ServiceContract]
    public interface ITrefoilService
    {
        [OperationContract]
        void AddBusinessFunction(BusinessFunctionDto dto);

        [OperationContract]
        void AddBusinessStatus(BusinessStatusDto dto);

        [OperationContract]
        void AddBusinessType(BusinessTypeDto dto);

        [OperationContract]
        int AddNoteByOrganisationId(int organisationid, int? createdby, NoteDto dto);

        [OperationContract]
        int AddNoteByProjectId(int projectid, int? createdby, NoteDto dto);

        [OperationContract]
        int AddNoteByTaskId(int taskid, int? createdby, NoteDto dto);

        [OperationContract]
        int AddOrganisation(OrganisationDto dto);

        [OperationContract]
        int AddProject(ProjectDto dto);

        [OperationContract]
        int AddTask(TaskDto dto);

        [OperationContract]
        void AddTaskAssignment(int taskid, int userid);

        [OperationContract]
        int AddUser(int organisationid, UserDto dto);

        [OperationContract]
        void DeleteOrganisationByOrganisationId(int organisationid);

        [OperationContract]
        void DeleteProjectByProjectId(int projectid);

        [OperationContract]
        void DeleteTaskByTaskId(int taskid);

        [OperationContract]
        IEnumerable<BusinessFunctionDto> GetBusinessFunctionDtoList();

        [OperationContract]
        IEnumerable<BusinessStatusDto> GetBusinessStatusDtoList();

        [OperationContract]
        IEnumerable<BusinessTypeDto> GetBusinessTypeDtoList();

        [OperationContract]
        IEnumerable<BusinessValueDto> GetBusinessValueDtoList();

        [OperationContract]
        IEnumerable<NoteDto> GetNoteDtoListByOrganisationId(int organisationid);

        [OperationContract]
        IEnumerable<NoteDto> GetNoteDtoListByProjectId(int projectid);

        [OperationContract]
        IEnumerable<NoteDto> GetNoteDtoListByTaskId(int taskid);

        [OperationContract]
        OrganisationDto GetOrganisationDtoByOrganisationId(int organisationid);

        [OperationContract]
        IEnumerable<OrganisationDto> GetOrganisationDtoList();

        [OperationContract]
        IEnumerable<PriorityTypeDto> GetPriorityTypeDtoList();

        [OperationContract]
        ProjectDto GetProjectDtoByProjectId(int projectid);

        [OperationContract]
        IEnumerable<ProjectDto> GetProjectDtoList();

        [OperationContract]
        IEnumerable<ProjectDto> GetProjectDtoListByOrganisationId(int organisationid);

        [OperationContract]
        IEnumerable<StateTypeDto> GetStateTypeDtoList();

        [OperationContract]
        IEnumerable<StatusTypeDto> GetStatusTypeDtoList();

        [OperationContract]
        TaskDto GetTaskDtoByTaskId(int taskid);

        [OperationContract]
        IEnumerable<TaskDto> GetTaskDtoList();

        [OperationContract]
        IEnumerable<TaskDto> GetTaskDtoListByOrganisationId(int organisationid);

        [OperationContract]
        IEnumerable<TaskDto> GetTaskDtoListByProjectId(int projectid);

        [OperationContract]
        UserDto GetUserDtoByEmail(string emailaddress);

        [OperationContract]
        UserDto GetUserDtoByUserId(int userid);

        [OperationContract]
        IEnumerable<UserDto> GetUserDtoList();

        [OperationContract]
        IEnumerable<UserDto> GetUserDtoListByInternal();

        [OperationContract]
        IEnumerable<UserDto> GetUserDtoListByOrganisationId(int organisationid);

        [OperationContract]
        IEnumerable<UserDto> GetUserDtoListByTaskId(int taskid);

        [OperationContract]
        void UpdateBusinessFunctionByBusinessFunctionId(int businessfunctionid, BusinessFunctionDto dto);

        [OperationContract]
        void UpdateBusinessStatusByBusinessStatusId(int businessstatusid, BusinessStatusDto dto);

        [OperationContract]
        void UpdateBusinessTypeByBusinessTypeId(int businesstypeid, BusinessTypeDto dto);

        [OperationContract]
        void UpdateNoteByNoteId(int noteid, int? modifiedby, NoteDto dto);

        [OperationContract]
        void UpdateOrganisationByOrganisationId(int organisationid, int? modifiedby, OrganisationDto dto);

        [OperationContract]
        void UpdateProjectActualTimeSpentByProjectId(int projectid, int? modifiedby, DateTime? actualstart, DateTime? actualend, int? actualdurationminutes);

        [OperationContract]
        void UpdateProjectByProjectId(int projectid, int? modifiedby, ProjectDto dto);

        [OperationContract]
        void UpdateProjectStatusAsCancelledByProjectId(int projectid, int? modifiedby, string cancelledreason);

        [OperationContract]
        void UpdateProjectStatusAsRejectedByProjectId(int projectid, int? modifiedby, string rejectedreason);

        [OperationContract]
        void UpdateTaskActualTimeSpentByTaskId(int taskid, int? modifiedby, DateTime? actualstart, DateTime? actualend, int? actualdurationminutes);

        [OperationContract]
        void UpdateTaskByTaskId(int taskid, int? modifiedby, TaskDto dto);

        [OperationContract]
        void UpdateUserByUserId(int systemuserid, int? modifiedby, UserDto dto);

        [OperationContract]
        bool ValidateLogin(UserDto dto);
    }
}
