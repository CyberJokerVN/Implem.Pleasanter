﻿using Implem.DefinitionAccessor;
using Implem.Libraries.Classes;
using Implem.Libraries.DataSources.SqlServer;
using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.DataTypes;
using Implem.Pleasanter.Libraries.Extensions;
using Implem.Pleasanter.Libraries.General;
using Implem.Pleasanter.Libraries.Html;
using Implem.Pleasanter.Libraries.HtmlParts;
using Implem.Pleasanter.Libraries.Models;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Server;
using Implem.Pleasanter.Libraries.ServerScripts;
using Implem.Pleasanter.Libraries.Settings;
using Implem.Pleasanter.Models.ApiSiteSettings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using static Implem.Pleasanter.Libraries.ServerScripts.ServerScriptModel;
namespace Implem.Pleasanter.Models
{
    [Serializable]
    public class ResultModel : BaseItemModel
    {
        public long ResultId = 0;
        public Status Status = new Status();
        public User Manager = new User();
        public User Owner = new User();
        public bool Locked = false;

        public TitleBody TitleBody
        {
            get
            {
                return new TitleBody(ResultId, Ver, VerType == Versions.VerTypes.History, Title.Value, Title.DisplayValue, Body);
            }
        }

        public SiteTitle SiteTitle
        {
            get
            {
                return new SiteTitle(SiteId);
            }
        }

        public long SavedResultId = 0;
        public int SavedStatus = 0;
        public int SavedManager = 0;
        public int SavedOwner = 0;
        public bool SavedLocked = false;

        public bool Status_Updated(Context context, bool copy = false, Column column = null)
        {
            if (copy && column?.CopyByDefault == true)
            {
                return column.GetDefaultInput(context: context).ToInt() != Status.Value;
            }
            return Status.Value != SavedStatus
                &&  (column == null
                    || column.DefaultInput.IsNullOrEmpty()
                    || column.GetDefaultInput(context: context).ToInt() != Status.Value);
        }

        public bool Manager_Updated(Context context, bool copy = false, Column column = null)
        {
            if (copy && column?.CopyByDefault == true)
            {
                return column.GetDefaultInput(context: context).ToInt() != Manager.Id;
            }
            return Manager.Id != SavedManager
                &&  (column == null
                    || column.DefaultInput.IsNullOrEmpty()
                    || column.GetDefaultInput(context: context).ToInt() != Manager.Id);
        }

        public bool Owner_Updated(Context context, bool copy = false, Column column = null)
        {
            if (copy && column?.CopyByDefault == true)
            {
                return column.GetDefaultInput(context: context).ToInt() != Owner.Id;
            }
            return Owner.Id != SavedOwner
                &&  (column == null
                    || column.DefaultInput.IsNullOrEmpty()
                    || column.GetDefaultInput(context: context).ToInt() != Owner.Id);
        }

        public bool Locked_Updated(Context context, bool copy = false, Column column = null)
        {
            if (copy && column?.CopyByDefault == true)
            {
                return column.GetDefaultInput(context: context).ToBool() != Locked;
            }
            return Locked != SavedLocked
                &&  (column == null
                    || column.DefaultInput.IsNullOrEmpty()
                    || column.GetDefaultInput(context: context).ToBool() != Locked);
        }

        public string PropertyValue(Context context, Column column)
        {
            switch (column?.ColumnName)
            {
                case "SiteId": return SiteId.ToString();
                case "UpdatedTime": return UpdatedTime.Value.ToString();
                case "ResultId": return ResultId.ToString();
                case "Ver": return Ver.ToString();
                case "Title": return Title.Value;
                case "Body": return Body;
                case "TitleBody": return TitleBody.ToString();
                case "Status": return Status.Value.ToString();
                case "Manager": return Manager.Id.ToString();
                case "Owner": return Owner.Id.ToString();
                case "Locked": return Locked.ToString();
                case "SiteTitle": return SiteTitle.SiteId.ToString();
                case "Comments": return Comments.ToJson();
                case "Creator": return Creator.Id.ToString();
                case "Updator": return Updator.Id.ToString();
                case "CreatedTime": return CreatedTime.Value.ToString();
                case "VerUp": return VerUp.ToString();
                case "Timestamp": return Timestamp;
                default: return GetValue(
                    context: context,
                    column: column);
            }
        }

        public string SavedPropertyValue(Context context, Column column)
        {
            switch (column?.ColumnName)
            {
                case "SiteId": return SavedSiteId.ToString();
                case "UpdatedTime": return SavedUpdatedTime.ToString();
                case "ResultId": return SavedResultId.ToString();
                case "Ver": return SavedVer.ToString();
                case "Title": return SavedTitle;
                case "Body": return SavedBody;
                case "Status": return SavedStatus.ToString();
                case "Manager": return SavedManager.ToString();
                case "Owner": return SavedOwner.ToString();
                case "Locked": return SavedLocked.ToString();
                case "Comments": return SavedComments;
                case "Creator": return SavedCreator.ToString();
                case "Updator": return SavedUpdator.ToString();
                case "CreatedTime": return SavedCreatedTime.ToString();
                default: return GetSavedValue(
                    context: context,
                    column: column);
            }
        }

        public Dictionary<string, string> PropertyValues(Context context, List<Column> columns)
        {
            var hash = new Dictionary<string, string>();
            columns?
                .Where(column => column != null)
                .ForEach(column =>
                {
                    switch (column.ColumnName)
                    {
                        case "SiteId":
                            hash.Add("SiteId", SiteId.ToString());
                            break;
                        case "UpdatedTime":
                            hash.Add("UpdatedTime", UpdatedTime.Value.ToString());
                            break;
                        case "ResultId":
                            hash.Add("ResultId", ResultId.ToString());
                            break;
                        case "Ver":
                            hash.Add("Ver", Ver.ToString());
                            break;
                        case "Title":
                            hash.Add("Title", Title.Value);
                            break;
                        case "Body":
                            hash.Add("Body", Body);
                            break;
                        case "TitleBody":
                            hash.Add("TitleBody", TitleBody.ToString());
                            break;
                        case "Status":
                            hash.Add("Status", Status.Value.ToString());
                            break;
                        case "Manager":
                            hash.Add("Manager", Manager.Id.ToString());
                            break;
                        case "Owner":
                            hash.Add("Owner", Owner.Id.ToString());
                            break;
                        case "Locked":
                            hash.Add("Locked", Locked.ToString());
                            break;
                        case "SiteTitle":
                            hash.Add("SiteTitle", SiteTitle.SiteId.ToString());
                            break;
                        case "Comments":
                            hash.Add("Comments", Comments.ToJson());
                            break;
                        case "Creator":
                            hash.Add("Creator", Creator.Id.ToString());
                            break;
                        case "Updator":
                            hash.Add("Updator", Updator.Id.ToString());
                            break;
                        case "CreatedTime":
                            hash.Add("CreatedTime", CreatedTime.Value.ToString());
                            break;
                        case "VerUp":
                            hash.Add("VerUp", VerUp.ToString());
                            break;
                        case "Timestamp":
                            hash.Add("Timestamp", Timestamp);
                            break;
                        default:
                            hash.Add(column.ColumnName, GetValue(
                                context: context,
                                column: column));
                            break;
                    }
                });
            return hash;
        }

        public bool PropertyUpdated(Context context, string name)
        {
            switch (name)
            {
                case "SiteId": return SiteId_Updated(context: context);
                case "Ver": return Ver_Updated(context: context);
                case "Title": return Title_Updated(context: context);
                case "Body": return Body_Updated(context: context);
                case "Status": return Status_Updated(context: context);
                case "Manager": return Manager_Updated(context: context);
                case "Owner": return Owner_Updated(context: context);
                case "Locked": return Locked_Updated(context: context);
                case "Comments": return Comments_Updated(context: context);
                case "Creator": return Creator_Updated(context: context);
                case "Updator": return Updator_Updated(context: context);
                default: 
                    switch (Def.ExtendedColumnTypes.Get(name ?? string.Empty))
                    {
                        case "Class": return Class_Updated(name);
                        case "Num": return Num_Updated(name);
                        case "Date": return Date_Updated(name);
                        case "Description": return Description_Updated(name);
                        case "Check": return Check_Updated(name);
                        case "Attachments": return Attachments_Updated(name);
                    }
                    break;
            }
            return false;
        }

        public string CsvData(
            Context context,
            SiteSettings ss,
            Column column,
            ExportColumn exportColumn,
            List<string> mine,
            bool? encloseDoubleQuotes)
        {
            var value = string.Empty;
            switch (column.Name)
            {
                case "SiteId":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? SiteId.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "ResultId":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? ResultId.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Ver":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Ver.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Title":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Title.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Body":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Body.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "TitleBody":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? TitleBody.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Status":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Status.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Manager":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Manager.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Owner":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Owner.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Locked":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Locked.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "SiteTitle":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? SiteTitle.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Comments":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Comments.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Creator":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Creator.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "Updator":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? Updator.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "CreatedTime":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? CreatedTime.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                            : string.Empty;
                    break;
                case "UpdatedTime":
                    value = ss.ReadColumnAccessControls.Allowed(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ? UpdatedTime?.ToExport(
                                context: context,
                                column: column,
                                exportColumn: exportColumn)
                                    ?? String.Empty
                            : string.Empty;
                    break;
                default:
                    switch (Def.ExtendedColumnTypes.Get(column?.Name ?? string.Empty))
                    {
                        case "Class":
                            value = ss.ReadColumnAccessControls.Allowed(
                                context: context,
                                ss: ss,
                                column: column,
                                mine: mine)
                                    ? GetClass(columnName: column.Name).ToExport(
                                        context: context,
                                        column: column,
                                        exportColumn: exportColumn)
                                    : string.Empty;
                            break;
                        case "Num":
                            value = ss.ReadColumnAccessControls.Allowed(
                                context: context,
                                ss: ss,
                                column: column,
                                mine: mine)
                                    ? GetNum(columnName: column.Name).ToExport(
                                        context: context,
                                        column: column,
                                        exportColumn: exportColumn)
                                    : string.Empty;
                            break;
                        case "Date":
                            value = ss.ReadColumnAccessControls.Allowed(
                                context: context,
                                ss: ss,
                                column: column,
                                mine: mine)
                                    ? GetDate(columnName: column.Name).ToExport(
                                        context: context,
                                        column: column,
                                        exportColumn: exportColumn)
                                    : string.Empty;
                            break;
                        case "Description":
                            value = ss.ReadColumnAccessControls.Allowed(
                                context: context,
                                ss: ss,
                                column: column,
                                mine: mine)
                                    ? GetDescription(columnName: column.Name).ToExport(
                                        context: context,
                                        column: column,
                                        exportColumn: exportColumn)
                                    : string.Empty;
                            break;
                        case "Check":
                            value = ss.ReadColumnAccessControls.Allowed(
                                context: context,
                                ss: ss,
                                column: column,
                                mine: mine)
                                    ? GetCheck(columnName: column.Name).ToExport(
                                        context: context,
                                        column: column,
                                        exportColumn: exportColumn)
                                    : string.Empty;
                            break;
                        case "Attachments":
                            value = ss.ReadColumnAccessControls.Allowed(
                                context: context,
                                ss: ss,
                                column: column,
                                mine: mine)
                                    ? GetAttachments(columnName: column.Name).ToExport(
                                        context: context,
                                        column: column,
                                        exportColumn: exportColumn)
                                    : string.Empty;
                            break;
                        default: return string.Empty;
                    }
                    break;
            }
            return CsvUtilities.EncloseDoubleQuotes(
                value: value,
                encloseDoubleQuotes: encloseDoubleQuotes);
        }

        public List<long> SwitchTargets;

        public ResultModel()
        {
        }

        public ResultModel(
            Context context,
            SiteSettings ss,
            Dictionary<string, string> formData = null,
            ResultApiModel resultApiModel = null,
            MethodTypes methodType = MethodTypes.NotSet)
        {
            OnConstructing(context: context);
            SetDefault(
                context: context,
                ss: ss);
            SiteId = ss.SiteId;
            if (formData != null)
            {
                SetByForm(
                    context: context,
                    ss: ss,
                    formData: formData);
            }
            if (resultApiModel != null)
            {
                SetByApi(context: context, ss: ss, data: resultApiModel);
            }
            if (formData != null || resultApiModel != null)
            {
                SetByLookups(
                    context: context,
                    ss: ss,
                    formData: formData);
            }
            SetByStatusControls(
                context: context,
                ss: ss);
            MethodType = methodType;
            OnConstructed(context: context);
        }

        public ResultModel(
            Context context,
            SiteSettings ss,
            long resultId,
            View view = null,
            bool setCopyDefault = false,
            Dictionary<string, string> formData = null,
            ResultApiModel resultApiModel = null,
            SqlColumnCollection column = null,
            bool clearSessions = false,
            List<long> switchTargets = null,
            MethodTypes methodType = MethodTypes.NotSet)
        {
            OnConstructing(context: context);
            SetDefault(
                context: context,
                ss: ss);
            ResultId = resultId;
            SiteId = ss.SiteId;
            if (context.QueryStrings.ContainsKey("ver"))
            {
                Get(
                    context: context,
                    tableType: Sqls.TableTypes.NormalAndHistory,
                    column: column,
                    where: Rds.ResultsWhereDefault(
                        context: context,
                        resultModel: this)
                            .Results_Ver(context.QueryStrings.Int("ver")), ss: ss);
            }
            else
            {
                Get(
                    context: context,
                    ss: ss,
                    view: view,
                    column: column);
            }
            if (setCopyDefault)
            {
                SetCopyDefault(
                    context: context,
                    ss: ss);
            }
            if (clearSessions) ClearSessions(context: context);
            if (formData != null)
            {
                SetByForm(
                    context: context,
                    ss: ss,
                    formData: formData);
            }
            if (resultApiModel != null)
            {
                SetByApi(context: context, ss: ss, data: resultApiModel);
            }
            if (formData != null || resultApiModel != null)
            {
                SetByLookups(
                    context: context,
                    ss: ss,
                    formData: formData);
            }
            SetByStatusControls(
                context: context,
                ss: ss);
            if (SavedLocked)
            {
                ss.SetLockedRecord(
                    context: context,
                    time: UpdatedTime,
                    user: Updator);
            }
            SwitchTargets = switchTargets;
            MethodType = methodType;
            OnConstructed(context: context);
        }

        public ResultModel(
            Context context,
            SiteSettings ss,
            DataRow dataRow,
            Dictionary<string, string> formData = null,
            string tableAlias = null)
        {
            OnConstructing(context: context);
            if (dataRow != null)
            {
                Set(
                    context: context,
                    ss: ss,
                    dataRow: dataRow,
                    tableAlias: tableAlias);
            }
            if (formData != null)
            {
                SetByForm(
                    context: context,
                    ss: ss,
                    formData: formData);
            }
            if (formData != null)
            {
                SetByLookups(
                    context: context,
                    ss: ss,
                    formData: formData);
            }
            SetByStatusControls(
                context: context,
                ss: ss);
            OnConstructed(context: context);
        }

        private void OnConstructing(Context context)
        {
        }

        private void OnConstructed(Context context)
        {
        }

        public void ClearSessions(Context context)
        {
        }

        public ResultModel Get(
            Context context,
            SiteSettings ss,
            View view = null,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal,
            SqlColumnCollection column = null,
            SqlJoinCollection join = null,
            SqlWhereCollection where = null,
            SqlOrderByCollection orderBy = null,
            SqlParamCollection param = null,
            bool distinct = false,
            int top = 0)
        {
            where = (view != null)
                ? Rds.ResultsWhere().SiteId(SiteId)
                : where ?? Rds.ResultsWhereDefault(
                    context: context,
                    resultModel: this);
            view = view ?? new View();
            view.SetColumnsWhere(
                context: context,
                ss: ss,
                where: where,
                siteId: SiteId,
                id: ResultId,
                timestamp: Timestamp.ToDateTime());
            column = (column ?? Rds.ResultsEditorColumns(ss))?.SetExtendedSqlSelectingColumn(context: context, ss: ss, view: view);
            join = join ?? Rds.ResultsJoinDefault();
            if (ss?.TableType == Sqls.TableTypes.Normal)
            {
                join = ss.Join(
                    context: context,
                    join: new Implem.Libraries.DataSources.Interfaces.IJoin[]
                    {
                        column,
                        where,
                        orderBy
                    });
            }
            Set(context, ss, Repository.ExecuteTable(
                context: context,
                statements: Rds.SelectResults(
                    tableType: tableType,
                    column: column,
                    join: join,
                    where: where,
                    orderBy: orderBy,
                    param: param,
                    distinct: distinct,
                    top: top)));
            return this;
        }

        public ResultApiModel GetByApi(Context context, SiteSettings ss)
        {
            var data = new ResultApiModel()
            {
                ApiVersion = context.ApiVersion
            };
            ss.ReadableColumns(context: context, noJoined: true).ForEach(column =>
            {
                switch (column.ColumnName)
                {
                    case "SiteId": data.SiteId = SiteId; break;
                    case "UpdatedTime": data.UpdatedTime = UpdatedTime.Value.ToLocal(context: context); break;
                    case "ResultId": data.ResultId = ResultId; break;
                    case "Ver": data.Ver = Ver; break;
                    case "Title": data.Title = Title.Value; break;
                    case "Body": data.Body = Body; break;
                    case "Status": data.Status = Status.Value; break;
                    case "Manager": data.Manager = Manager.Id; break;
                    case "Owner": data.Owner = Owner.Id; break;
                    case "Locked": data.Locked = Locked; break;
                    case "Creator": data.Creator = Creator.Id; break;
                    case "Updator": data.Updator = Updator.Id; break;
                    case "CreatedTime": data.CreatedTime = CreatedTime.Value.ToLocal(context: context); break;
                    case "Comments": data.Comments = Comments.ToLocal(context: context).ToJson(); break;
                    default: 
                        data.Value(
                            context: context,
                            column: column,
                            value: GetValue(
                                context: context,
                                column: column,
                                toLocal: true));
                        break;
                }
            });
            data.ItemTitle = Title.DisplayValue;
            return data;
        }

        public string ToValue(Context context, SiteSettings ss, Column column, List<string> mine)
        {
            if (!ss.ReadColumnAccessControls.Allowed(
                context: context,
                ss: ss,
                column: column,
                mine: mine))
            {
                return string.Empty;
            }
            switch (column.TypeName)
            {
                case "datetime":
                    return ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: mine)
                            ?.ToString() ?? string.Empty;
                default:
                    return PropertyValue(
                        context: context,
                        column: column);
            }
        }

        public string ToDisplay(Context context, SiteSettings ss, Column column, List<string> mine)
        {
            if (!ss.ReadColumnAccessControls.Allowed(
                context: context,
                ss: ss,
                column: column,
                mine: mine))
            {
                return string.Empty;
            }
            switch (column.Name)
            {
                case "ResultId":
                    return ResultId.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Title":
                    return Title.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Body":
                    return Body.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Status":
                    return Status.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Manager":
                    return Manager.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Owner":
                    return Owner.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Locked":
                    return Locked.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Timestamp":
                    return Timestamp.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "TitleBody":
                    return TitleBody.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Ver":
                    return Ver.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Comments":
                    return Comments.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Creator":
                    return Creator.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "Updator":
                    return Updator.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "CreatedTime":
                    return CreatedTime.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                case "UpdatedTime":
                    return UpdatedTime.ToDisplay(
                        context: context,
                        ss: ss,
                        column: column);
                default:
                    switch (Def.ExtendedColumnTypes.Get(column?.Name ?? string.Empty))
                    {
                        case "Class":
                            return GetClass(columnName: column.Name).ToDisplay(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Num":
                            return GetNum(columnName: column.Name).ToDisplay(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Date":
                            return GetDate(columnName: column.Name).ToDisplay(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Description":
                            return GetDescription(columnName: column.Name).ToDisplay(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Check":
                            return GetCheck(columnName: column.Name).ToDisplay(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Attachments":
                            return GetAttachments(columnName: column.Name).ToDisplay(
                                context: context,
                                ss: ss,
                                column: column);
                        default:
                            return string.Empty;
                    }
            }
        }

        public object ToApiDisplayValue(Context context, SiteSettings ss, Column column, List<string> mine)
        {
            if (!ss.ReadColumnAccessControls.Allowed(
                context: context,
                ss: ss,
                column: column,
                mine: mine))
            {
                return string.Empty;
            }
            switch (column.Name)
            {
                case "SiteId":
                    return SiteId.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "UpdatedTime":
                    return UpdatedTime.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "ResultId":
                    return ResultId.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Ver":
                    return Ver.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Title":
                    return Title.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Body":
                    return Body.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "TitleBody":
                    return TitleBody.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Status":
                    return Status.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Manager":
                    return Manager.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Owner":
                    return Owner.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Locked":
                    return Locked.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "SiteTitle":
                    return SiteTitle.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Comments":
                    return Comments.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Creator":
                    return Creator.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Updator":
                    return Updator.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "CreatedTime":
                    return CreatedTime.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "VerUp":
                    return VerUp.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Timestamp":
                    return Timestamp.ToApiDisplayValue(
                        context: context,
                        ss: ss,
                        column: column);
                default:
                    switch (Def.ExtendedColumnTypes.Get(column?.Name ?? string.Empty))
                    {
                        case "Class":
                            return GetClass(columnName: column.Name).ToApiDisplayValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Num":
                            return GetNum(columnName: column.Name).ToApiDisplayValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Date":
                            return GetDate(columnName: column.Name).ToApiDisplayValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Description":
                            return GetDescription(columnName: column.Name).ToApiDisplayValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Check":
                            return GetCheck(columnName: column.Name).ToApiDisplayValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Attachments":
                            return GetAttachments(columnName: column.Name).ToApiDisplayValue(
                                context: context,
                                ss: ss,
                                column: column);
                        default:
                            return string.Empty;
                    }
            }
        }

        public object ToApiValue(Context context, SiteSettings ss, Column column, List<string> mine)
        {
            if (!ss.ReadColumnAccessControls.Allowed(
                context: context,
                ss: ss,
                column: column,
                mine: mine))
            {
                return string.Empty;
            }
            switch (column.Name)
            {
                case "SiteId":
                    return SiteId.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "UpdatedTime":
                    return UpdatedTime.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "ResultId":
                    return ResultId.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Ver":
                    return Ver.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Title":
                    return Title.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Body":
                    return Body.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "TitleBody":
                    return TitleBody.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Status":
                    return Status.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Manager":
                    return Manager.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Owner":
                    return Owner.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Locked":
                    return Locked.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "SiteTitle":
                    return SiteTitle.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Comments":
                    return Comments.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Creator":
                    return Creator.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Updator":
                    return Updator.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "CreatedTime":
                    return CreatedTime.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "VerUp":
                    return VerUp.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                case "Timestamp":
                    return Timestamp.ToApiValue(
                        context: context,
                        ss: ss,
                        column: column);
                default:
                    switch (Def.ExtendedColumnTypes.Get(column?.Name ?? string.Empty))
                    {
                        case "Class":
                            return GetClass(columnName: column.Name).ToApiValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Num":
                            return GetNum(columnName: column.Name).ToApiValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Date":
                            return GetDate(columnName: column.Name).ToApiValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Description":
                            return GetDescription(columnName: column.Name).ToApiValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Check":
                            return GetCheck(columnName: column.Name).ToApiValue(
                                context: context,
                                ss: ss,
                                column: column);
                        case "Attachments":
                            return GetAttachments(columnName: column.Name).ToApiValue(
                                context: context,
                                ss: ss,
                                column: column);
                        default:
                            return string.Empty;
                    }
            }
        }

        public string FullText(
            Context context,
            SiteSettings ss,
            bool backgroundTask = false,
            bool onCreating = false)
        {
            if (!Parameters.Search.CreateIndexes && !backgroundTask) return null;
            if (AccessStatus == Databases.AccessStatuses.NotFound) return null;
            var fullText = new System.Text.StringBuilder();
            if (ss.FullTextIncludeBreadcrumb == true)
            {
                SiteInfo.TenantCaches
                    .Get(context.TenantId)?
                    .SiteMenu.Breadcrumb(
                        context: context,
                        siteId: SiteId)
                    .FullText(
                        context: context,
                        fullText: fullText);
            }
            if (ss.FullTextIncludeSiteId == true)
            {
                fullText.Append($" {ss.SiteId}");
            }
            if (ss.FullTextIncludeSiteTitle == true)
            {
                fullText.Append($" {ss.Title}");
            }
            ss.GetEditorColumnNames(
                context: context,
                columnOnly: true)
                    .Select(columnName => ss.GetColumn(
                        context: context,
                        columnName: columnName))
                    .ForEach(column =>
                    {
                        switch (column.ColumnName)
                        {
                            case "ResultId":
                                ResultId.FullText(
                                    context: context,
                                    column: column,
                                    fullText: fullText);
                                break;
                            case "Title":
                                Title.FullText(
                                    context: context,
                                    column: column,
                                    fullText: fullText);
                                break;
                            case "Body":
                                Body.FullText(
                                    context: context,
                                    column: column,
                                    fullText: fullText);
                                break;
                            case "Status":
                                Status.FullText(
                                    context: context,
                                    column: column,
                                    fullText: fullText);
                                break;
                            case "Manager":
                                Manager.FullText(
                                    context: context,
                                    column: column,
                                    fullText: fullText);
                                break;
                            case "Owner":
                                Owner.FullText(
                                    context: context,
                                    column: column,
                                    fullText: fullText);
                                break;
                            case "Comments":
                                Comments.FullText(
                                    context: context,
                                    column: column,
                                    fullText: fullText);
                                break;
                            default:
                                BaseFullText(
                                    context: context,
                                    column: column,
                                    fullText: fullText);
                                break;
                        }
                    });
            Creator.FullText(
                context,
                column: ss.GetColumn(
                    context: context,
                    columnName: "Creator"),
                fullText);
            Updator.FullText(
                context,
                column: ss.GetColumn(
                    context: context,
                    columnName: "Updator"),
                fullText);
            CreatedTime.FullText(
                context,
                column: ss.GetColumn(
                    context: context,
                    columnName: "CreatedTime"),
                fullText);
            UpdatedTime.FullText(
                context,
                column: ss.GetColumn(
                    context: context,
                    columnName: "UpdatedTime"),
                fullText);
            if (!onCreating)
            {
                FullTextExtensions.OutgoingMailsFullText(
                    context: context,
                    ss: ss,
                    fullText: fullText,
                    referenceType: "Results",
                    referenceId: ResultId);
            }
            return fullText
                .ToString()
                .Replace("　", " ")
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Split(' ')
                .Select(o => o.Trim())
                .Where(o => o != string.Empty)
                .Distinct()
                .Join(" ");
        }

        public ErrorData Create(
            Context context,
            SiteSettings ss,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal,
            SqlParamCollection param = null,
            List<Process> processes = null,
            long copyFrom = 0,
            bool extendedSqls = true,
            bool synchronizeSummary = true,
            bool forceSynchronizeSourceSummary = false,
            bool notice = false,
            bool migrationMode = false,
            string noticeType = "Created",
            bool otherInitValue = false,
            bool get = true)
        {
            SetByBeforeCreateServerScript(
                context: context,
                ss: ss);
            if (context.ErrorData.Type != Error.Types.None)
            {
                return context.ErrorData;
            }
            var statements = new List<SqlStatement>();
            if (extendedSqls)
            {
                statements.OnCreatingExtendedSqls(
                    context: context,
                    siteId: SiteId);
            }
            statements.AddRange(CreateStatements(
                context: context,
                ss: ss,
                tableType: tableType,
                param: param,
                migrationMode: migrationMode,
                otherInitValue: otherInitValue));
            try
            {
                WriteAttachmentsToLocal(
                    context: context,
                    ss: ss);
            }
            catch
            {
                return new ErrorData(
                    type: Error.Types.FailedWriteFile,
                    id: ResultId);
            }
            var response = Repository.ExecuteScalar_response(
                context: context,
                transactional: true,
                selectIdentity: true,
                statements: statements.ToArray());
            if (response.Event == "Duplicated")
            {
                return new ErrorData(
                    type: Error.Types.Duplicated,
                    id: ResultId,
                    columnName: response.ColumnName);
            }
            DeleteTempOrLocalAttachments(
                context: context,
                ss: ss);
            ResultId = (response.Id ?? ResultId).ToLong();
            if (synchronizeSummary)
            {
                SynchronizeSummary(
                    context: context,
                    ss: ss,
                    force: forceSynchronizeSourceSummary);
            }
            ExecuteAutomaticNumbering(
                context: context,
                ss: ss);
            processes?
                .Where(process => process.MatchConditions)
                .ForEach(process =>
                    ExecuteAutomaticNumbering(
                        context: context,
                        ss: ss,
                        autoNumbering: process.AutoNumbering));
            if (context.ContractSettings.Notice != false && notice)
            {
                SetTitle(
                    context: context,
                    ss: ss);
                Notice(
                    context: context,
                    ss: ss,
                    notifications: GetNotifications(
                        context: context,
                        ss: ss,
                        notice: notice),
                    type: noticeType);
                processes?
                    .Where(process => process.MatchConditions)
                    .ForEach(process =>
                        process?.Notifications?.ForEach(notification =>
                            notification.Send(
                                context: context,
                                ss: ss,
                                title: ReplacedDisplayValues(
                                    context: context,
                                    ss: ss,
                                    value: notification.Subject),
                                body: ReplacedDisplayValues(
                                    context: context,
                                    ss: ss,
                                    value: notification.Body),
                                valuesTo: ss.IncludedColumns(notification.Address)
                                    .ToDictionary(
                                        column => column,
                                        column => PropertyValue(
                                            context: context,
                                            column: column)),
                                valuesCc: ss.IncludedColumns(notification.CcAddress)
                                    .ToDictionary(
                                        column => column,
                                        column => PropertyValue(
                                            context: context,
                                            column: column)),
                                valuesBcc: ss.IncludedColumns(notification.BccAddress)
                                    .ToDictionary(
                                        column => column,
                                        column => PropertyValue(
                                            context: context,
                                            column: column)))));
            }
            if (get) Get(context: context, ss: ss);
            if (ss.PermissionForCreating != null)
            {
                ss.SetPermissions(
                    context: context,
                    referenceId: ResultId);
            }
            var fullText = FullText(context, ss: ss, onCreating: true);
            statements = new List<SqlStatement>();
            statements.Add(Rds.UpdateItems(
                param: Rds.ItemsParam()
                    .Title(Title.DisplayValue)
                    .FullText(fullText, _using: fullText != null)
                    .SearchIndexCreatedTime(DateTime.Now, _using: fullText != null),
                where: Rds.ItemsWhere().ReferenceId(ResultId)));
            statements.Add(BinaryUtilities.UpdateReferenceId(
                context: context,
                ss: ss,
                referenceId: ResultId,
                values: fullText));
            if (extendedSqls)
            {
                statements.OnCreatedExtendedSqls(
                    context: context,
                    siteId: SiteId,
                    id: ResultId);
            }
            Repository.ExecuteNonQuery(
                context: context,
                transactional: true,
                statements: statements.ToArray());
            if (get && Rds.ExtendedSqls(
                context: context,
                siteId: SiteId,
                id: ResultId)
                    ?.Any(o => o.OnCreated) == true)
            {
                Get(
                    context: context,
                    ss: ss);
            }
            if (copyFrom > 0)
            {
                ss.LinkActions(
                    context: context,
                    type: "CopyWithLinks",
                    data: new Dictionary<string, string>()
                    {
                        { "From", copyFrom.ToString() },
                        { "To", ResultId.ToString() }
                    });
            }
            SetByAfterCreateServerScript(
                context: context,
                ss: ss);
            return new ErrorData(type: Error.Types.None);
        }

        public List<SqlStatement> CreateStatements(
            Context context,
            SiteSettings ss,
            string dataTableName = null,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal,
            SqlParamCollection param = null,
            bool migrationMode = false,
            bool otherInitValue = false)
        {
            var statements = new List<SqlStatement>();
            statements.AddRange(IfDuplicatedStatements(ss: ss));
            statements.AddRange(new List<SqlStatement>
            {
                Rds.InsertItems(
                    dataTableName: dataTableName,
                    selectIdentity: true,
                    param: Rds.ItemsParam()
                        .ReferenceType("Results")
                        .SiteId(SiteId)
                        .Title(Title.DisplayValue)
                        .MigrationParams(
                            creator: Creator?.Id,
                            updator: Updator?.Id,
                            createdTime: CreatedTime?.Value,
                            updatedTime: UpdatedTime?.Value,
                            _using: migrationMode),
                    addUpdatorParam: !migrationMode),
                Rds.InsertResults(
                    dataTableName: dataTableName,
                    tableType: tableType,
                    param: param ?? Rds.ResultsParamDefault(
                        context: context,
                        ss: ss,
                        resultModel: this,
                        setDefault: true,
                        otherInitValue: otherInitValue)
                            .MigrationParams(
                                creator: Creator?.Id,
                                updator: Updator?.Id,
                                createdTime: CreatedTime?.Value,
                                updatedTime: UpdatedTime?.Value,
                                _using: migrationMode),
                    addUpdatorParam: !migrationMode),
                InsertLinks(
                    context: context,
                    ss: ss,
                    setIdentity: true),
            });
            statements.AddRange(UpdateAttachmentsStatements(
                context: context,
                ss: ss));
            statements.AddRange(PermissionUtilities.InsertStatements(
                context: context,
                ss: ss,
                columns: ss.Columns
                    .Where(o => o.Type != Column.Types.Normal)
                    .ToDictionary(
                        o => $"{o.ColumnName},{o.Type}",
                        o => (o.MultipleSelections == true
                            ? PropertyValue(
                                context: context,
                                column: o)?.Deserialize<List<int>>()
                            : PropertyValue(
                                context: context,
                                column: o)?.ToInt().ToSingleList()) ?? new List<int>()),
                permissions: ss.PermissionForCreating));
            return statements;
        }

        public void ExecuteAutomaticNumbering(
            Context context,
            SiteSettings ss)
        {
            ss.Columns
                .Where(column => !column.AutoNumberingFormat.IsNullOrEmpty())
                .Where(column => !column.Joined)
                .ForEach(column => ExecuteAutomaticNumbering(
                    context: context,
                    ss: ss,
                    autoNumbering: new AutoNumbering()
                    {
                        ColumnName = column.ColumnName,
                        Format = column.AutoNumberingFormat,
                        ResetType = column.AutoNumberingResetType,
                        Default = column.AutoNumberingDefault,
                        Step = column.AutoNumberingStep
                    }));
        }

        private void ExecuteAutomaticNumbering(
            Context context,
            SiteSettings ss,
            AutoNumbering autoNumbering,
            bool overwrite = true)
        {
            if (autoNumbering == null)
            {
                return;
            }
            var column = ss.GetColumn(
                context: context,
                columnName: autoNumbering.ColumnName);
            if (column == null)
            {
                return;
            }
            if (!overwrite
                && !GetValue(
                    context: context,
                    column: column).IsNullOrEmpty())
            {
                return;
            }
            SetByForm(
                context: context,
                ss: ss,
                formData: new Dictionary<string, string>()
                {
                    {
                        $"Results_{autoNumbering.ColumnName}",
                        AutoNumberingUtilities.ExecuteAutomaticNumbering(
                            context: context,
                            ss: ss,
                            autoNumbering: autoNumbering,
                            data: ss.IncludedColumns(value: autoNumbering.Format)
                                .ToDictionary(
                                    o => o.ColumnName,
                                    o => ToDisplay(
                                        context: context,
                                        ss: ss,
                                        column: o,
                                        mine: Mine(context: context))),
                            updateModel: Rds.UpdateResults(
                                where: Rds.ResultsWhere()
                                    .SiteId(SiteId)
                                    .ResultId(ResultId)))
                    }
                });
        }

        public ErrorData Update(
            Context context,
            SiteSettings ss,
            List<Process> processes = null,
            bool extendedSqls = true,
            bool synchronizeSummary = true,
            bool forceSynchronizeSourceSummary = false,
            bool notice = false,
            string previousTitle = null,
            SqlParamCollection param = null,
            List<SqlStatement> additionalStatements = null,
            bool otherInitValue = false,
            bool setBySession = true,
            bool get = true,
            bool checkConflict = true)
        {
            SetByBeforeUpdateServerScript(
                context: context,
                ss: ss);
            if (context.ErrorData.Type != Error.Types.None)
            {
                return context.ErrorData;
            }
            var notifications = GetNotifications(
                context: context,
                ss: ss,
                notice: notice,
                before: true);
            if (setBySession)
            {
                SetBySession(context: context);
            }
            var statements = new List<SqlStatement>();
            if (extendedSqls)
            {
                statements.OnUpdatingExtendedSqls(
                    context: context,
                    siteId: SiteId,
                    id: ResultId,
                    timestamp: Timestamp.ToDateTime());
            }
            var verUp = Versions.VerUp(
                context: context,
                ss: ss,
                verUp: VerUp);
            statements.AddRange(UpdateStatements(
                context: context,
                ss: ss,
                param: param,
                otherInitValue: otherInitValue,
                additionalStatements: additionalStatements,
                checkConflict: checkConflict,
                verUp: verUp));
            var response = Repository.ExecuteScalar_response(
                context: context,
                transactional: true,
                statements: statements.ToArray());
            if (response.Event == "Duplicated")
            {
                return new ErrorData(
                    type: Error.Types.Duplicated,
                    id: ResultId,
                    columnName: response.ColumnName);
            }
            if (response.Event == "Conflicted")
            {
                return new ErrorData(
                    type: Error.Types.UpdateConflicts,
                    id: ResultId);
            }
            processes?
                .Where(process => process.MatchConditions)
                .ForEach(process =>
                    ExecuteAutomaticNumbering(
                        context: context,
                        ss: ss,
                        autoNumbering: process.AutoNumbering,
                        overwrite: false));
            WriteAttachments(
                context: context,
                ss: ss,
                verUp: verUp);
            if (synchronizeSummary)
            {
                SynchronizeSummary(
                    context: context,
                    ss: ss,
                    force: forceSynchronizeSourceSummary);
            }
            if (context.ContractSettings.Notice != false && notice)
            {
                Notice(
                    context: context,
                    ss: ss,
                    notifications: NotificationUtilities.MeetConditions(
                        ss: ss,
                        before: notifications,
                        after: GetNotifications(
                            context: context,
                            ss: ss,
                            notice: notice)),
                    type: "Updated");
                processes?
                    .Where(process => process.MatchConditions)
                    .ForEach(process =>
                        process?.Notifications?.ForEach(notification =>
                            notification.Send(
                                context: context,
                                ss: ss,
                                title: ReplacedDisplayValues(
                                    context: context,
                                    ss: ss,
                                    value: notification.Subject),
                                body: ReplacedDisplayValues(
                                    context: context,
                                    ss: ss,
                                    value: notification.Body),
                                valuesTo: ss.IncludedColumns(notification.Address)
                                    .ToDictionary(
                                        column => column,
                                        column => PropertyValue(
                                            context: context,
                                            column: column)),
                                valuesCc: ss.IncludedColumns(notification.CcAddress)
                                    .ToDictionary(
                                        column => column,
                                        column => PropertyValue(
                                            context: context,
                                            column: column)),
                                valuesBcc: ss.IncludedColumns(notification.BccAddress)
                                    .ToDictionary(
                                        column => column,
                                        column => PropertyValue(
                                            context: context,
                                            column: column)))));
            }
            if (get)
            {
                Get(context: context, ss: ss);
            }
            UpdateRelatedRecords(
                context: context,
                ss: ss,
                extendedSqls: extendedSqls,
                previousTitle: previousTitle,
                get: get,
                addUpdatedTimeParam: true,
                addUpdatorParam: true,
                updateItems: true);
            SetByAfterUpdateServerScript(
                context: context,
                ss: ss);
            SetByStatusControls(
                context: context,
                ss: ss,
                force: true);
            return new ErrorData(type: Error.Types.None);
        }

        public List<SqlStatement> UpdateStatements(
            Context context,
            SiteSettings ss,
            string dataTableName = null,
            SqlParamCollection param = null,
            bool otherInitValue = false,
            List<SqlStatement> additionalStatements = null,
            bool checkConflict = true,
            bool verUp = false)
        {
            ss.Columns
                .Where(column => column.ColumnName.StartsWith("Attachments"))
                .ForEach(column => GetAttachments(column.ColumnName).SetData(
                    context: context,
                    column: column));
            var timestamp = Timestamp.ToDateTime();
            var statements = new List<SqlStatement>();
            var where = Rds.ResultsWhereDefault(
                context: context,
                resultModel: this)
                    .UpdatedTime(timestamp, _using: timestamp.InRange() && checkConflict);
            statements.AddRange(IfDuplicatedStatements(ss: ss));
            if (verUp)
            {
                statements.Add(Rds.ResultsCopyToStatement(
                    where: where,
                    tableType: Sqls.TableTypes.History,
                    ColumnNames()));
                Ver++;
            }
            statements.AddRange(UpdateStatements(
                context: context,
                ss: ss,
                dataTableName: dataTableName,
                where: where,
                param: param,
                otherInitValue: otherInitValue));
            statements.AddRange(UpdateAttachmentsStatements(
                context: context,
                ss: ss,
                verUp: verUp));
            if (ss.PermissionForUpdating?.Any() == true)
            {
                statements.AddRange(PermissionUtilities.UpdateStatements(
                    context: context,
                    ss: ss,
                    referenceId: ResultId,
                    columns: ss.Columns
                        .Where(o => o.Type != Column.Types.Normal)
                        .ToDictionary(
                            o => $"{o.ColumnName},{o.Type}",
                            o => (o.MultipleSelections == true
                                ? PropertyValue(
                                    context: context,
                                    column: o)?.Deserialize<List<int>>()
                                : PropertyValue(
                                    context: context,
                                    column: o)?.ToInt().ToSingleList()) ?? new List<int>()),
                    permissions: ss.PermissionForUpdating));
            }
            else if (RecordPermissions != null)
            {
                statements.UpdatePermissions(
                    context: context,
                    ss: ss,
                    referenceId: ResultId,
                    permissions: RecordPermissions);
            }
            if (additionalStatements?.Any() == true)
            {
                statements.AddRange(additionalStatements);
            }
            return statements;
        }

        private List<SqlStatement> UpdateStatements(
            Context context,
            SiteSettings ss,
            string dataTableName = null,
            SqlWhereCollection where = null,
            SqlParamCollection param = null,
            bool otherInitValue = false)
        {
            return new List<SqlStatement>
            {
                Rds.UpdateResults(
                    dataTableName: dataTableName,
                    where: where,
                    param: param ?? Rds.ResultsParamDefault(
                        context: context,
                        ss: ss,
                        resultModel: this,
                        otherInitValue: otherInitValue)),
                new SqlStatement()
                {
                    DataTableName = dataTableName,
                    IfConflicted = true,
                    Id = ResultId
                }
            };
        }

        private List<SqlStatement> UpdateAttachmentsStatements(Context context, SiteSettings ss, bool verUp = false)
        {
            var statements = new List<SqlStatement>();
            ColumnNames()
                .Where(columnName => columnName.StartsWith("Attachments"))
                .Where(columnName => Attachments_Updated(columnName: columnName))
                .ForEach(columnName =>
                    GetAttachments(columnName: columnName).Statements(
                        context: context,
                        ss: ss,
                        column: ss.GetColumn(
                            context: context,
                            columnName: columnName),
                        statements: statements,
                        referenceId: ResultId,
                        verUp: verUp));
            return statements;
        }

        public void WriteAttachments(Context context, SiteSettings ss, bool verUp = false)
        {
            ColumnNames()
                .Where(columnName => columnName.StartsWith("Attachments"))
                .Where(columnName => Attachments_Updated(columnName: columnName))
                .ForEach(columnName =>
                    GetAttachments(columnName: columnName).Write(
                        context: context,
                        ss: ss,
                        column: ss.GetColumn(
                            context: context,
                            columnName: columnName),
                        referenceId: ResultId,
                        verUp: verUp));
        }

        public void WriteAttachmentsToLocal(Context context, SiteSettings ss)
        {
            ColumnNames()
                .Where(columnName => columnName.StartsWith("Attachments"))
                .Where(columnName => Attachments_Updated(columnName: columnName))
                .ForEach(columnName =>
                    GetAttachments(columnName: columnName).WriteToLocal(
                        context: context,
                        column: ss.GetColumn(
                            context: context,
                            columnName: columnName)));
        }

        public void DeleteTempOrLocalAttachments(Context context, SiteSettings ss, bool verUp = false)
        {
            ColumnNames()
                .Where(columnName => columnName.StartsWith("Attachments"))
                .Where(columnName => Attachments_Updated(columnName: columnName))
                .ForEach(columnName =>
                    GetAttachments(columnName: columnName).DeleteTempOrLocalAttachment(
                        context: context,
                        ss: ss,
                        column: ss.GetColumn(
                            context: context,
                            columnName: columnName),
                        referenceId: ResultId,
                        verUp: verUp));
        }

        public void UpdateRelatedRecords(
            Context context,
            SiteSettings ss,
            bool extendedSqls = false,
            bool get = false,
            string previousTitle = null,
            bool addUpdatedTimeParam = true,
            bool addUpdatorParam = true,
            bool updateItems = true)
        {
            Repository.ExecuteNonQuery(
                context: context,
                transactional: true,
                statements: UpdateRelatedRecordsStatements(
                    context: context,
                    ss: ss,
                    extendedSqls: extendedSqls,
                    addUpdatedTimeParam: addUpdatedTimeParam,
                    addUpdatorParam: addUpdatorParam,
                    updateItems: updateItems)
                        .ToArray());
            var titleUpdated = Title_Updated(context: context);
            if (get && Rds.ExtendedSqls(
                context: context,
                siteId: SiteId,
                id: ResultId)
                    ?.Any(o => o.OnUpdated) == true)
            {
                Get(
                    context: context,
                    ss: ss);
            }
            if (previousTitle != null
                && previousTitle != Title.DisplayValue
                && ss.Sources?.Any() == true)
            {
                ItemUtilities.UpdateSourceTitles(
                    context: context,
                    ss: ss,
                    siteIdList: new List<long>() { ss.SiteId },
                    idList: ResultId.ToSingleList());
            }
        }

        public List<SqlStatement> UpdateRelatedRecordsStatements(
            Context context,
            SiteSettings ss,
            bool extendedSqls = false,
            bool addUpdatedTimeParam = true,
            bool addUpdatorParam = true,
            bool updateItems = true)
        {
            var fullText = FullText(context, ss: ss);
            var statements = new List<SqlStatement>();
            statements.Add(Rds.UpdateItems(
                where: Rds.ItemsWhere().ReferenceId(ResultId),
                param: Rds.ItemsParam()
                    .SiteId(SiteId)
                    .Title(Title.DisplayValue)
                    .FullText(fullText, _using: fullText != null)
                    .SearchIndexCreatedTime(DateTime.Now, _using: fullText != null),
                addUpdatedTimeParam: addUpdatedTimeParam,
                addUpdatorParam: addUpdatorParam,
                _using: updateItems));
            statements.Add(Rds.PhysicalDeleteLinks(
                where: Rds.LinksWhere().SourceId(ResultId)));
            statements.Add(InsertLinks(
                context: context,
                ss: ss));
            if (extendedSqls)
            {
                statements.OnUpdatedExtendedSqls(
                    context: context,
                    siteId: SiteId,
                    id: ResultId);
            }
            return statements;
        }

        private SqlInsert InsertLinks(Context context, SiteSettings ss, bool setIdentity = false)
        {
            var link = ss.Links
                ?.Where(o => o.SiteId > 0)
                .Where(o => ss.Destinations.ContainsKey(o.SiteId))
                .Select(o => ss.GetColumn(
                    context: context,
                    columnName: o.ColumnName))
                .Where(o => o != null)
                .SelectMany(column => column.MultipleSelections == true
                    ? GetClass(column).Deserialize<List<long>>()
                        ?? new List<long>()
                    : GetClass(column).ToLong().ToSingleList())
                .Where(id => id > 0)
                .Distinct()
                .ToDictionary(id => id, id => ResultId);
            return LinkUtilities.Insert(link, setIdentity);
        }

        public ErrorData UpdateOrCreate(
            Context context,
            SiteSettings ss,
            string dataTableName = null,
            SqlWhereCollection where = null,
            SqlParamCollection param = null)
        {
            SetBySession(context: context);
            var statements = new List<SqlStatement>
            {
                Rds.UpdateOrInsertResults(
                    where: where ?? Rds.ResultsWhereDefault(
                        context: context,
                        resultModel: this),
                    param: param ?? Rds.ResultsParamDefault(
                        context: context,
                        ss: ss,
                        resultModel: this,
                        setDefault: true))
            };
            var response = Repository.ExecuteScalar_response(
                context: context,
                transactional: true,
                selectIdentity: true,
                statements: statements.ToArray());
            ResultId = (response.Id ?? ResultId).ToLong();
            Get(context: context, ss: ss);
            return new ErrorData(type: Error.Types.None);
        }

        public ErrorData Move(Context context, SiteSettings ss, SiteSettings targetSs)
        {
            SiteId = targetSs.SiteId;
            var statements = new List<SqlStatement>();
            var fullText = FullText(
                context: context,
                ss: targetSs);
            statements.AddRange(IfDuplicatedStatements(ss: targetSs));
            statements.AddRange(new List<SqlStatement>
            {
                Rds.UpdateItems(
                    where: Rds.ItemsWhere().ReferenceId(ResultId),
                    param: Rds.ItemsParam()
                        .SiteId(SiteId)
                        .FullText(fullText, _using: fullText != null)),
                Rds.UpdateResults(
                    where: Rds.ResultsWhere().ResultId(ResultId),
                    param: Rds.ResultsParam().SiteId(SiteId))
            });
            var response = Repository.ExecuteScalar_response(
                context: context,
                transactional: true,
                statements: statements.ToArray());
            if (response?.Event == "Duplicated")
            {
                return new ErrorData(
                    type: Error.Types.Duplicated,
                    id: ResultId,
                    columnName: response.ColumnName);
            }
            SynchronizeSummary(
                context: context,
                ss: ss);
            Get(
                context: context,
                ss: targetSs);
            return new ErrorData(type: Error.Types.None);
        }

        public ErrorData Delete(Context context, SiteSettings ss, bool notice = false)
        {
            SetByBeforeDeleteServerScript(
                context: context,
                ss: ss);
            if (context.ErrorData.Type != Error.Types.None)
            {
                return context.ErrorData;
            }
            var notifications = context.ContractSettings.Notice != false && notice
                ? GetNotifications(
                    context: context,
                    ss: ss,
                    notice: notice)
                : null;
            ss.LinkActions(
                context: context,
                type: "DeleteWithLinks",
                sub: Rds.SelectResults(
                    column: Rds.ResultsColumn().ResultId(),
                    where: Rds.ResultsWhere()
                        .SiteId(ss.SiteId)
                        .ResultId(ResultId)));
            var statements = new List<SqlStatement>();
            var where = Rds.ResultsWhere().SiteId(SiteId).ResultId(ResultId);
            statements.OnDeletingExtendedSqls(
                context: context,
                siteId: SiteId,
                id: ResultId);
            statements.AddRange(new List<SqlStatement>
            {
                Rds.DeleteItems(
                    factory: context,
                    where: Rds.ItemsWhere().ReferenceId(ResultId)),
                Rds.DeleteBinaries(
                    factory: context,
                    where: Rds.BinariesWhere()
                        .TenantId(context.TenantId)
                        .ReferenceId(ResultId)
                        .BinaryType(
                            value: "Images",
                            _operator: "<>",
                            _using: ss.DeleteImageWhenDeleting == false)),
                Rds.DeleteResults(
                    factory: context,
                    where: where)
            });
            if (Parameters.BinaryStorage.RestoreLocalFiles == false)
            {
                ColumnNames()
                    .Where(columnName => columnName.StartsWith("Attachments"))
                    .ForEach(columnName =>
                    {
                        var attachments = GetAttachments(columnName: columnName);
                        attachments.ForEach(attachment =>
                            attachment.Deleted = true);
                        attachments.Statements(
                            context: context,
                            ss: ss,
                            column: ss.GetColumn(
                                context: context,
                                columnName: columnName),
                            statements: statements,
                            referenceId: ResultId);
                    });
            }
            statements.OnDeletedExtendedSqls(
                context: context,
                siteId: SiteId,
                id: ResultId);
            Repository.ExecuteNonQuery(
                context: context,
                transactional: true,
                statements: statements.ToArray());
            if (ss.DeleteImageWhenDeleting == false)
            {
                BinaryUtilities.UpdateImageReferenceId(
                    context: context,
                    siteId: SiteId,
                    referenceId: ResultId);
            }
            WriteAttachments(
                context: context,
                ss: ss);
            SynchronizeSummary(context, ss);
            if (context.ContractSettings.Notice != false && notice)
            {
                Notice(
                    context: context,
                    ss: ss,
                    notifications: notifications,
                    type: "Deleted");
            }
            SetByAfterDeleteServerScript(
                context: context,
                ss: ss);
            return new ErrorData(type: Error.Types.None);
        }

        public ErrorData Restore(Context context, SiteSettings ss,long resultId)
        {
            ResultId = resultId;
            Repository.ExecuteNonQuery(
                context: context,
                connectionString: Parameters.Rds.OwnerConnectionString,
                transactional: true,
                statements: new SqlStatement[]
                {
                    Rds.RestoreItems(
                        factory: context,
                        where: Rds.ItemsWhere().ReferenceId(ResultId)),
                    Rds.RestoreResults(
                        factory: context,
                        where: Rds.ResultsWhere().ResultId(ResultId))
                });
            return new ErrorData(type: Error.Types.None);
        }

        public ErrorData PhysicalDelete(
            Context context, SiteSettings ss,Sqls.TableTypes tableType = Sqls.TableTypes.Normal)
        {
            Repository.ExecuteNonQuery(
                context: context,
                transactional: true,
                statements: Rds.PhysicalDeleteResults(
                    tableType: tableType,
                    where: Rds.ResultsWhere().SiteId(SiteId).ResultId(ResultId)));
            return new ErrorData(type: Error.Types.None);
        }

        private List<SqlStatement> IfDuplicatedStatements(SiteSettings ss)
        {
            var statements = new List<SqlStatement>();
            ss.Columns
                .Where(column => column.NoDuplication == true)
                .ForEach(column =>
                {
                    var param = new Rds.ResultsParamCollection();
                    switch (column.ColumnName)
                    {
                        case "Title":
                            if (Title.Value != string.Empty)
                                statements.Add(column.IfDuplicatedStatement(
                                    param.Title(Title.Value.MaxLength(1024)), SiteId, ResultId));
                            break;
                        case "Body":
                            if (Body != string.Empty)
                                statements.Add(column.IfDuplicatedStatement(
                                    param.Body(Body), SiteId, ResultId));
                            break;
                        case "Status":
                            if (Status.Value != 0)
                                statements.Add(column.IfDuplicatedStatement(
                                    param.Status(Status.Value), SiteId, ResultId));
                            break;
                        case "Manager":
                            if (Manager.Id != 0)
                                statements.Add(column.IfDuplicatedStatement(
                                    param.Manager(Manager.Id), SiteId, ResultId));
                            break;
                        case "Owner":
                            if (Owner.Id != 0)
                                statements.Add(column.IfDuplicatedStatement(
                                    param.Owner(Owner.Id), SiteId, ResultId));
                            break;
                        default:
                            switch (Def.ExtendedColumnTypes.Get(column?.ColumnName ?? string.Empty))
                            {
                                case "Class":
                                    if (!GetClass(column: column).IsNullOrEmpty())
                                        statements.Add(column.IfDuplicatedStatement(
                                            param: param.Add(
                                                columnBracket: $"\"{column.ColumnName}\"",
                                                name: column.ColumnName,
                                                value: GetClass(column: column).MaxLength(1024)),
                                            siteId: SiteId,
                                            referenceId: ResultId));
                                    break;
                                case "Num":
                                    var num = GetNum(column: column);
                                    if (column.Nullable == true)
                                    {
                                        if (num?.Value != null)
                                            statements.Add(column.IfDuplicatedStatement(
                                                param: param.Add(
                                                    columnBracket: $"\"{column.ColumnName}\"",
                                                    name: column.ColumnName,
                                                    value: num.Value),
                                                siteId: SiteId,
                                                referenceId: ResultId));
                                    }
                                    else
                                    {
                                        if (num?.Value != null && num?.Value != 0)
                                            statements.Add(column.IfDuplicatedStatement(
                                                param: param.Add(
                                                    columnBracket: $"\"{column.ColumnName}\"",
                                                    name: column.ColumnName,
                                                    value: num.Value),
                                                siteId: SiteId,
                                                referenceId: ResultId));
                                    }
                                    break;
                                case "Date":
                                    if (GetDate(column: column) != 0.ToDateTime())
                                        statements.Add(column.IfDuplicatedStatement(
                                            param: param.Add(
                                                columnBracket: $"\"{column.ColumnName}\"",
                                                name: column.ColumnName,
                                                value: GetDate(column: column)),
                                            siteId: SiteId,
                                            referenceId: ResultId));
                                    break;
                                case "Description":
                                    if (!GetDescription(column: column).IsNullOrEmpty())
                                        statements.Add(column.IfDuplicatedStatement(
                                            param: param.Add(
                                                columnBracket: $"\"{column.ColumnName}\"",
                                                name: column.ColumnName,
                                                value: GetDescription(column: column)),
                                            siteId: SiteId,
                                            referenceId: ResultId));
                                    break;
                            }
                            break;
                    }
                });
            return statements;
        }

        public void SetDefault(
            Context context,
            SiteSettings ss)
        {
            ss.Columns
                .Where(o => !o.DefaultInput.IsNullOrEmpty())
                .ForEach(column => SetDefault(
                    context: context,
                    ss: ss,
                    column: column));
        }

        public void SetCopyDefault(Context context, SiteSettings ss)
        {
            ss.Columns
                .Where(column => column.CopyByDefault == true
                    || column.TypeCs == "Attachments"
                    || !column.CanRead(
                        context: context,
                        ss: ss,
                        mine: Mine(context: context)))
                .ForEach(column => SetDefault(
                    context: context,
                    ss: ss,
                    column: column));
        }

        public void SetDefault(
            Context context,
            SiteSettings ss,
            Column column)
        {
            var defaultInput = column.GetDefaultInput(context: context);
            switch (column.ColumnName)
            {
                case "Title":
                    Title.Value = defaultInput.ToString();
                    break;
                case "Body":
                    Body = defaultInput.ToString();
                    break;
                case "Status":
                    Status.Value = defaultInput.ToInt();
                    break;
                case "Locked":
                    Locked = defaultInput.ToBool();
                    break;
                case "Manager":
                    Manager = SiteInfo.User(
                        context: context,
                        userId: column.GetDefaultInput(context: context).ToInt());
                    break;
                case "Owner":
                    Owner = SiteInfo.User(
                        context: context,
                        userId: column.GetDefaultInput(context: context).ToInt());
                    break;
                default:
                    switch (Def.ExtendedColumnTypes.Get(column?.ColumnName ?? string.Empty))
                    {
                        case "Class":
                            SetClass(
                                column: column,
                                value: defaultInput);
                            break;
                        case "Num":
                            SetNum(
                                column: column,
                                value: new Num(
                                    context: context,
                                    column: column,
                                    value: defaultInput));
                            break;
                        case "Date":
                            SetDate(
                                column: column,
                                value: column.DefaultTime(context: context));
                            break;
                        case "Description":
                            SetDescription(
                                column: column,
                                value: defaultInput.ToString());
                            break;
                        case "Check":
                            SetCheck(
                                column: column,
                                value: defaultInput.ToBool());
                            break;
                        case "Attachments":
                            SetAttachments(
                                column: column,
                                value: new Attachments());
                            break;
                    }
                    break;
            }
        }

        public void SetByForm(
            Context context,
            SiteSettings ss,
            Dictionary<string, string> formData)
        {
            var postInitForm = formData
                .Where(o => o.Key.StartsWith("PostInit_"))
                .ToDictionary(o => o.Key.Replace("PostInit_", ""), o => o.Value);
            if (postInitForm.Count > 0)
            {
                SetByFormData(
                    context: context,
                    ss: ss,
                    formData: postInitForm);
            }
            SetByFormData(
                context: context,
                ss: ss,
                formData: formData);
            if (context.QueryStrings.ContainsKey("ver"))
            {
                Ver = context.QueryStrings.Int("ver");
            }
            var formsSiteId = context.RequestData("FromSiteId").ToLong();
            var notReturnParentRecord = context.RequestData("NotReturnParentRecord").ToBool();
            if (formsSiteId > 0 && !notReturnParentRecord)
            {
                var column = ss.GetColumn(
                    context: context,
                    columnName: ss.Links
                        ?.Where(o => o.SiteId > 0)
                        .FirstOrDefault(o => o.SiteId == formsSiteId).ColumnName);
                if (column != null)
                {
                    var value = PropertyValue(
                        context: context,
                        column: column);
                    column.Linking = column.MultipleSelections == true
                        ? value.Deserialize<List<string>>()?.Contains(context.RequestData("LinkId")) == true
                        : value == context.RequestData("LinkId");
                }
            }
            var queryStringsSiteId = context.RequestData("FromSiteId").ToLong();
            if (queryStringsSiteId > 0)
            {
                var id = context.RequestData("LinkId");
                ss.Links
                    ?.Where(link => link.SiteId == queryStringsSiteId)
                    .Where(link => ss.Links?.Any(o => o.SelectNewLink == true) != true
                        || link.SelectNewLink == true)
                    .Select(link => ss.GetColumn(
                        context: context,
                        columnName: link.ColumnName))
                    .Where(column => column != null)
                    .Where(column => !formData.Any(o => o.Key == $"Results_{column.ColumnName}"))
                    .ForEach(column =>
                    {
                        id = column.MultipleSelections == true
                            ? id.ToSingleList().ToJson()
                            : id;
                        SetClass(column.ColumnName, id);
                        column.ControlCss += " always-send";
                    });
            }
            SetByFormula(context: context, ss: ss);
            SetChoiceHash(context: context, ss: ss);
            if (context.Action == "deletecomment")
            {
                DeleteCommentId = formData.Get("ControlId")?
                    .Split(',')
                    ._2nd()
                    .ToInt() ?? 0;
                Comments.RemoveAll(o => o.CommentId == DeleteCommentId);
            }
        }

        private void SetByFormData(Context context, SiteSettings ss, Dictionary<string, string> formData)
        {
            formData.ForEach(data =>
            {
                var key = data.Key;
                var value = data.Value ?? string.Empty;
                switch (key)
                {
                    case "Results_Title": Title = new Title(ResultId, value); break;
                    case "Results_Body": Body = value.ToString(); break;
                    case "Results_Status": Status = new Status(value.ToInt());; break;
                    case "Results_Manager": Manager = SiteInfo.User(context: context, userId: value.ToInt()); break;
                    case "Results_Owner": Owner = SiteInfo.User(context: context, userId: value.ToInt()); break;
                    case "Results_Locked": Locked = value.ToBool(); break;
                    case "Results_Timestamp": Timestamp = value.ToString(); break;
                    case "Comments": Comments.Prepend(
                        context: context,
                        ss: ss,
                        body: value); break;
                    case "VerUp": VerUp = value.ToBool(); break;
                    case "CurrentPermissionsAll":
                        RecordPermissions = context.Forms.List("CurrentPermissionsAll");
                        break;
                    default:
                        if (key.RegexExists("Comment[0-9]+"))
                        {
                            Comments.Update(
                                context: context,
                                ss: ss,
                                commentId: key.Substring("Comment".Length).ToInt(),
                                body: value);
                        }
                        else
                        {
                            var column = ss.GetColumn(
                                context: context,
                                columnName: key.Split_2nd('_'));
                            switch (Def.ExtendedColumnTypes.Get(column?.ColumnName ?? string.Empty))
                            {
                                case "Class":
                                    SetClass(
                                        columnName: column.ColumnName,
                                        value: value);
                                    break;
                                case "Num":
                                    SetNum(
                                        columnName: column.ColumnName,
                                        value: new Num(
                                            context: context,
                                            column: column,
                                            value: value));
                                    break;
                                case "Date":
                                    SetDate(
                                        columnName: column.ColumnName,
                                        value: value.ToDateTime().ToUniversal(context: context));
                                    break;
                                case "Description":
                                    SetDescription(
                                        columnName: column.ColumnName,
                                        value: value);
                                    break;
                                case "Check":
                                    SetCheck(
                                        columnName: column.ColumnName,
                                        value: value.ToBool());
                                    break;
                                case "Attachments":
                                    SetAttachments(
                                        columnName: column.ColumnName,
                                        value: value.Deserialize<Attachments>());
                                    break;
                            }
                        }
                        break;
                }
            });
        }

        public void SetByCsvRow(
            Context context,
            SiteSettings ss,
            Dictionary<int, ImportColumn> columnHash,
            List<string> row,
            bool migrationMode = false)
        {
            columnHash
                .Where(column =>
                    (column.Value.Column.CanCreate(
                        context: context,
                        ss: ss,
                        mine: Mine(context: context))
                            && ResultId == 0)
                    || (column.Value.Column.CanUpdate(
                        context: context,
                        ss: ss,
                        mine: Mine(context: context))
                            && ResultId > 0))
                .ForEach(column =>
                {
                    var recordingData = ImportRecordingData(
                        context: context,
                        column: column.Value.Column,
                        value: ImportUtilities.RecordingData(
                            columnHash: columnHash,
                            row: row,
                            column: column),
                        inheritPermission: ss.InheritPermission);
                    switch (column.Value.Column.ColumnName)
                    {
                        case "Title":
                            Title.Value = recordingData.ToString();
                            break;
                        case "Body":
                            Body = recordingData.ToString();
                            break;
                        case "Status":
                            Status.Value = recordingData.ToInt();
                            break;
                        case "Locked":
                            Locked = recordingData.ToBool();
                            break;
                        case "Manager":
                            Manager = SiteInfo.User(
                                context: context,
                                userId: recordingData.ToInt());
                            break;
                        case "Owner":
                            Owner = SiteInfo.User(
                                context: context,
                                userId: recordingData.ToInt());
                            break;
                        case "Comments":
                            if (!row[column.Key].IsNullOrEmpty())
                            {
                                Comments.ClearAndSplitPrepend(
                                    context: context,
                                    ss: ss,
                                    body: row[column.Key],
                                    update: AccessStatus == Databases.AccessStatuses.Selected);
                            }
                            break;
                        default:
                            SetValue(
                                context: context,
                                column: column.Value.Column,
                                value: recordingData);
                            break;
                    }
                    if (migrationMode)
                    {
                        switch (column.Value.Column.ColumnName)
                        {
                            case "Creator":
                                Creator = SiteInfo.User(
                                    context: context,
                                    userId: recordingData.ToInt());
                                break;
                            case "Updator":
                                Updator = SiteInfo.User(
                                    context: context,
                                    userId: recordingData.ToInt());
                                break;
                            case "CreatedTime":
                                CreatedTime = new Time(
                                    context: context,
                                    value: recordingData.ToDateTime());
                                break;
                            case "UpdatedTime":
                                UpdatedTime = new Time(
                                    context: context,
                                    value: recordingData.ToDateTime());
                                break;
                        }
                    }
                });
            SetBySettings(
                context: context,
                ss: ss);
            SetByFormula(
                context: context,
                ss: ss);
            SetTitle(
                context: context,
                ss: ss);
        }

        private static string ImportRecordingData(
            Context context, Column column, string value, long inheritPermission)
        {
            var recordingData = column.RecordingData(
                context: context,
                value: value,
                siteId: inheritPermission);
            return recordingData;
        }

        public void SetProcessMatchConditions(
            Context context,
            SiteSettings ss)
        {
            ss.Processes?.ForEach(process =>
                process.MatchConditions = GetProcessMatchConditions(
                    context: context,
                    ss: ss,
                    process: process));
        }

        public bool GetProcessMatchConditions(
            Context context,
            SiteSettings ss,
            Process process)
        {
            return Matched(
                context: context,
                ss: ss,
                view: process.View)
                   && (process.CurrentStatus == -1
                        || Status.Value == process.CurrentStatus);
        }

        public StatusControl.ControlConstraintsTypes GetStatusControl(
            Context context,
            SiteSettings ss,
            Column column)
        {
            if (StatusControlHash == null)
            {
                SetByStatusControls(
                    context: context,
                    ss: ss);
            }
            return StatusControlHash.Get(column.ColumnName);
        }

        public ResultModel CopyAndInit(
            Context context,
            SiteSettings ss)
        {
            var resultModel = new ResultModel(
                context: context,
                ss: ss,
                methodType: MethodTypes.New);
            resultModel.SetByModel(this);
            resultModel.ResultId = 0;
            resultModel.Ver = 1;
            resultModel.Comments = new Comments();
            resultModel.AccessStatus = Databases.AccessStatuses.Initialized;
            resultModel.SetCopyDefault(
                context: context,
                ss: ss);
            resultModel.SetByForm(
                context: context,
                ss: ss,
                formData: context.Forms);
            resultModel.SetBySettings(
                context: context,
                ss: ss,
                formData: context.Forms);
            resultModel.SetByStatusControls(
                context: context,
                ss: ss,
                force: true);
            return resultModel;
        }

        public void SetByModel(ResultModel resultModel)
        {
            SiteId = resultModel.SiteId;
            UpdatedTime = resultModel.UpdatedTime;
            Title = resultModel.Title;
            Body = resultModel.Body;
            Status = resultModel.Status;
            Manager = resultModel.Manager;
            Owner = resultModel.Owner;
            Locked = resultModel.Locked;
            Comments = resultModel.Comments;
            Creator = resultModel.Creator;
            Updator = resultModel.Updator;
            CreatedTime = resultModel.CreatedTime;
            VerUp = resultModel.VerUp;
            Comments = resultModel.Comments;
            ClassHash = resultModel.ClassHash;
            NumHash = resultModel.NumHash;
            DateHash = resultModel.DateHash;
            DescriptionHash = resultModel.DescriptionHash;
            CheckHash = resultModel.CheckHash;
            AttachmentsHash = resultModel.AttachmentsHash;
        }

        public void SetByApi(Context context, SiteSettings ss, ResultApiModel data)
        {
            if (data.Title != null) Title = new Title(data.ResultId.ToLong(), data.Title);
            if (data.Body != null) Body = data.Body.ToString().ToString();
            if (data.Status != null) Status = new Status(data.Status.ToInt());;
            if (data.Manager != null) Manager = SiteInfo.User(context: context, userId: data.Manager.ToInt());
            if (data.Owner != null) Owner = SiteInfo.User(context: context, userId: data.Owner.ToInt());
            if (data.Locked != null) Locked = data.Locked.ToBool().ToBool();
            if (data.Comments != null) Comments.Prepend(context: context, ss: ss, body: data.Comments);
            if (data.VerUp != null) VerUp = data.VerUp.ToBool();
            data.ClassHash?.ForEach(o => SetClass(
                columnName: o.Key,
                value: o.Value));
            data.NumHash?.ForEach(o => SetNum(
                columnName: o.Key,
                value: new Num(
                    context: context,
                    column: ss.GetColumn(
                        context: context,
                        columnName: o.Key),
                    value: o.Value.ToString())));
            data.DateHash?.ForEach(o => SetDate(
                columnName: o.Key,
                value: o.Value.ToDateTime().ToUniversal(context: context)));
            data.DescriptionHash?.ForEach(o => SetDescription(
                columnName: o.Key,
                value: o.Value));
            data.CheckHash?.ForEach(o => SetCheck(
                columnName: o.Key,
                value: o.Value));
            data.AttachmentsHash?.ForEach(o =>
            {
                string columnName = o.Key;
                Attachments newAttachments = o.Value;
                Attachments oldAttachments;
                if (columnName == "Attachments#Uploading")
                {
                    var kvp = AttachmentsHash
                        .FirstOrDefault(x => x.Value
                            .Any(att => att.Guid == newAttachments.FirstOrDefault()?.Guid?.Split_1st()));
                    columnName = kvp.Key;
                    oldAttachments = kvp.Value;
                    var column = ss.GetColumn(
                        context: context,
                        columnName: columnName);
                    if (column.OverwriteSameFileName == true)
                    {
                        var oldAtt = oldAttachments
                            .FirstOrDefault(att => att.Guid == newAttachments.FirstOrDefault()?.Guid?.Split_1st());
                        if (oldAtt != null)
                        {
                            oldAtt.Deleted = true;
                            oldAtt.Overwritten = true;
                        }
                    }
                    newAttachments.ForEach(att => att.Guid = att.Guid.Split_2nd());
                }
                else
                {
                    oldAttachments = AttachmentsHash.Get(columnName);
                }
                if (oldAttachments != null)
                {
                    var column = ss.GetColumn(
                        context: context,
                        columnName: columnName);
                    var newGuidSet = new HashSet<string>(newAttachments.Select(x => x.Guid).Distinct());
                    var newNameSet = new HashSet<string>(newAttachments.Select(x => x.Name).Distinct());
                    newAttachments.ForEach(newAttachment =>
                    {
                        newAttachment.AttachmentAction(
                            context: context,
                            column: column,
                            oldAttachments: oldAttachments);
                    });
                    if (column.OverwriteSameFileName == true)
                    {
                        newAttachments.AddRange(oldAttachments.
                            Where((oldvalue) =>
                                !newGuidSet.Contains(oldvalue.Guid) &&
                                !newNameSet.Contains(oldvalue.Name)));
                    }
                    else
                    {
                        newAttachments.AddRange(oldAttachments.
                            Where((oldvalue) => !newGuidSet.Contains(oldvalue.Guid)));
                    }
                }
                SetAttachments(columnName: columnName, value: newAttachments);
            });
            if (ss.AllowMigrationMode == true
                && data.MigrationMode == true)
            {
                if (data.Creator != null)
                {
                    Creator = SiteInfo.User(
                        context: context,
                        userId: data.Creator.ToInt());
                }
                if (data.Updator != null)
                {
                    Updator = SiteInfo.User(
                    context: context,
                    userId: data.Updator.ToInt());
                }
                if (data.CreatedTime != null)
                {
                    CreatedTime = new Time(
                    context: context,
                    value: data.CreatedTime.ToDateTime());
                }
                if (data.UpdatedTime != null)
                {
                    UpdatedTime = new Time(
                    context: context,
                    value: data.UpdatedTime.ToDateTime());
                }
            }
            data.ImageHash?.ForEach(o =>
            {
                var bytes = Convert.FromBase64String(o.Value.Base64);
                var stream = new System.IO.MemoryStream(bytes);
                var file = new Microsoft.AspNetCore.Http.FormFile(stream, 0, bytes.Length, null, $"image{o.Value.Extension}");
                if (ss.ColumnHash.Get(o.Key).AllowImage == true)
                {
                    SetPostedFile(
                        context: context,
                        file: file,
                        columnName: o.Key,
                        image: o.Value);
                    SetImageValue(
                        context: context,
                        ss: ss,
                        columnName: o.Key,
                        imageApiModel: o.Value);
                }
            });
            RecordPermissions = data.RecordPermissions;
            SetByFormula(context: context, ss: ss);
            SetChoiceHash(context: context, ss: ss);
        }

        public void SetPostedFile(
            Context context,
            Microsoft.AspNetCore.Http.IFormFile file,
            string columnName,
            Shared._ImageApiModel image)
        {
            PostedImageHash.Add(
                columnName,
                new PostedFile()
                {
                    Guid = new HttpPostedFile(file).WriteToTemp(context),
                    FileName = file.FileName.Split(System.IO.Path.DirectorySeparatorChar).Last(),
                    Extension = image.Extension,
                    Size = file.Length,
                    ContentType = MimeKit.MimeTypes.GetMimeType(image.Extension),
                    ContentRange = file.Length > 0
                        ? new System.Net.Http.Headers.ContentRangeHeaderValue(
                            0,
                            file.Length - 1,
                            file.Length)
                        : new System.Net.Http.Headers.ContentRangeHeaderValue(0, 0, 0),
                    InputStream = file.OpenReadStream()
                });
        }

        public void SetImageValue(
            Context context,
            SiteSettings ss,
            string columnName,
            Shared._ImageApiModel imageApiModel)
        {
            var imageText = $"![{imageApiModel.Alt}]" + "({0})".Params(Locations.ShowFile(
                context: context,
                guid: PostedImageHash.Get(columnName).Guid));
            switch (columnName)
            {
                case "Body":
                    Body = InsertImageText(
                        body: Body,
                        imageText: imageText,
                        imageApiModel: imageApiModel);
                    break;
                case "Comments":
                    var comment = Comments.GetCreated(
                        context: context,
                        ss: ss);
                    comment.Body = InsertImageText(
                        body: comment.Body,
                        imageText: imageText,
                        imageApiModel: imageApiModel);
                    break;
                default:
                    if (Def.ExtendedColumnTypes.Get(columnName) == "Description")
                    {
                        if (!DescriptionHash.ContainsKey(columnName))
                        {
                            DescriptionHash.Add(columnName, string.Empty);
                        }
                        DescriptionHash[columnName] = InsertImageText(
                            body: DescriptionHash.Get(columnName),
                            imageText: imageText,
                            imageApiModel: imageApiModel);
                    }
                    break;
            }
        }

        public string InsertImageText(
            string body,
            string imageText,
            Shared._ImageApiModel imageApiModel)
        {
            if (imageApiModel.HeadNewLine == true)
            {
                imageText = $"\n{imageText}";
            }
            if (imageApiModel.EndNewLine == true)
            {
                imageText = $"{imageText}\n";
            }
            var insertedBody = imageApiModel.Position.ToInt() == -1
                ? body + imageText
                : imageApiModel.Position.ToInt() < body.Length
                    ? body.Insert(imageApiModel.Position.ToInt(), imageText)
                    : body + imageText;
            return insertedBody;
        }

        public void SetByProcess(
            Context context,
            SiteSettings ss,
            Process process)
        {
            if (process.ChangedStatus != -1)
            {
                Status.Value = process.ChangedStatus;
            }
            process.DataChanges?.ForEach(dataChange =>
            {
                var key = $"Results_{dataChange.ColumnName}";
                var formData = new Dictionary<string, string>();
                switch (dataChange.Type)
                {
                    case DataChange.Types.CopyValue:
                        formData[key] = ToValue(
                            context: context,
                            ss: ss,
                            column: ss.GetColumn(
                                context: context,
                                columnName: dataChange.Value),
                            mine: Mine(context: context));
                        break;
                    case DataChange.Types.CopyDisplayValue:
                        formData[key] = ToDisplay(
                            context: context,
                            ss: ss,
                            column: ss.GetColumn(
                                context: context,
                                columnName: dataChange.Value),
                            mine: Mine(context: context));
                        break;
                    case DataChange.Types.InputValue:
                        formData[key] = dataChange.ValueData(ss
                            .IncludedColumns(value: dataChange.Value)
                            .ToDictionary(
                                column => column.ColumnName,
                                column => ToDisplay(
                                    context: context,
                                    ss: ss,
                                    column: column,
                                    mine: Mine(context: context))));
                        break;
                    case DataChange.Types.InputValueFormula:
                        var formulaSet = new FormulaSet()
                        {
                            Id = dataChange.Id
                        };
                        var err = FormulaBuilder.SetFormula(
                            formulaSet: formulaSet,
                            ss: ss,
                            calculationMethod: "Extended",
                            target: dataChange.ColumnName,
                            condition: null,
                            formula: dataChange.Value,
                            notUseDisplayName: false,
                            isDisplayError: dataChange.ValueFormulaIsDisplayError,
                            outOfCondition: null);
                        if (err == Error.Types.None)
                        {
                            formData[key] = ExecFormulaExtended(
                                context: context,
                                ss: ss,
                                columnName: dataChange.ColumnName,
                                formulaSet: formulaSet,
                                isOutOfCondition: false,
                                outputFormulaLogs: ss.ProcessOutputFormulaLogs);
                        }
                        else
                        {
                            throw new Exception($"Formula error {formulaSet.Id}:{err.Message(context: context)}");
                        }
                        break;
                    case DataChange.Types.InputDate:
                    case DataChange.Types.InputDateTime:
                        var baseDateTimeColumn = ss.GetColumn(
                            context: context,
                            columnName: dataChange.BaseDateTime);
                        var baseDateTime = baseDateTimeColumn != null
                            ? ToValue(
                                context: context,
                                ss: ss,
                                column: baseDateTimeColumn,
                                mine: Mine(context: context)).ToDateTime()
                            : DateTime.MinValue;
                        formData[key] = dataChange.DateTimeValue(
                            context: context,
                            baseDateTime: baseDateTime);
                        break;
                    case DataChange.Types.InputUser:
                        formData[key] = context.UserId.ToString();
                        break;
                    case DataChange.Types.InputDept:
                        formData[key] = context.DeptId.ToString();
                        break;
                    default:
                        break;
                }
                SetByForm(
                    context: context,
                    ss: ss,
                    formData: formData);
                SetByLookups(
                    context: context,
                    ss: ss,
                    formData: formData);
            });
        }

        public void SetBySettings(
            Context context,
            SiteSettings ss,
            Dictionary<string, string> formData = null,
            bool copyByDefaultOnly = false)
        {
            SetByLookups(
                context: context,
                ss: ss,
                formData: formData,
                copyByDefaultOnly: copyByDefaultOnly);
            SetByStatusControls(
                context: context,
                ss: ss);
        }

        private void SetByLookups(
            Context context,
            SiteSettings ss,
            Dictionary<string,string> formData = null,
            bool copyByDefaultOnly = false)
        {
            var changedFormData = ss.Links
                .OrderBy(link => context.ControlledOrder?.Contains($"{ss.ReferenceType}_{link.ColumnName}") == true
                    ? context.ControlledOrder.IndexOf($"{ss.ReferenceType}_{link.ColumnName}")
                    : int.MaxValue)
                .Where(link => link.Lookups?.Any() == true)
                .Where(link => PropertyUpdated(
                    context: context,
                    name: link.ColumnName)
                        || context.Forms.ContainsKey($"{ss.ReferenceType}_{link.ColumnName}"))
                .SelectMany(link => link.Lookups?.LookupData(
                    context: context,
                    ss: ss,
                    link: link,
                    id: PropertyValue(
                        context: context,
                        column: ss.GetColumn(
                            context: context,
                            columnName: link.ColumnName)).ToLong(),
                    formData: formData,
                    blankColumns: link.Lookups
                        ?.Select(lookup => ss.GetColumn(
                            context: context,
                            columnName: lookup.To))
                        .Where(column => column?.BlankValue(value: AccessStatus == Databases.AccessStatuses.Selected
                            ? SavedPropertyValue(
                                context: context,
                                column: column)
                            : PropertyValue(
                                context: context,
                                column: column)) == true)
                        .Select(column => column.ColumnName)
                        .ToList(),
                    copyByDefaultOnly: copyByDefaultOnly))
                .GroupBy(o => o.Key)
                .Select(o => o.FirstOrDefault())
                .ToDictionary(o => o.Key, o => o.Value);
            if (changedFormData.Any())
            {
                SetByForm(
                    context: context,
                    ss: ss,
                    formData: changedFormData);
            }
        }

        private void SetByStatusControls(
            Context context,
            SiteSettings ss,
            bool force = false)
        {
            if (StatusControlHash == null || force)
            {
                StatusControlHash = new Dictionary<string, StatusControl.ControlConstraintsTypes>();
                ss.StatusControls?
                    .Where(statusControl => statusControl.Status == -1
                        || statusControl.Status == Status.Value)
                    .Where(statusControl => statusControl.Accessable(context: context))
                    .Where(statusControl => statusControl.View == null
                        || Matched(
                            context: context,
                            ss: ss,
                            view: statusControl.View))
                    .ForEach(statusControl =>
                    {
                        ReadOnly |= statusControl.ReadOnly == true;
                        statusControl.ColumnHash?.ForEach(data =>
                            StatusControlHash.AddIfNotConainsKey(data.Key, data.Value));
                    });
            }
        }

        public void SynchronizeSummary(Context context, SiteSettings ss, bool force = false)
        {
            ss.Summaries.ForEach(summary =>
            {
                var id = SynchronizeSummaryDestinationId(linkColumn: summary.LinkColumn);
                var savedId = SynchronizeSummaryDestinationId(
                    linkColumn: summary.LinkColumn,
                    saved: true);
                if (id != 0)
                {
                    SynchronizeSummary(
                        context: context,
                        ss: ss,
                        summary: summary,
                        id: id);
                }
                if (savedId != 0 && id != savedId)
                {
                    SynchronizeSummary(
                        context: context,
                        ss: ss,
                        summary: summary,
                        id: savedId);
                }
            });
            SynchronizeSourceSummary(
                context: context,
                ss: ss,
                force: force);
        }

        private void SynchronizeSummary(
            Context context, SiteSettings ss, Summary summary, long id)
        {
            var destinationSs = SiteSettingsUtilities.Get(
                context: context, siteId: summary.SiteId);
            if (destinationSs != null)
            {
                Summaries.Synchronize(
                    context: context,
                    ss: ss,
                    destinationSs: destinationSs,
                    destinationSiteId: summary.SiteId,
                    destinationColumn: summary.DestinationColumn,
                    destinationCondition: destinationSs.Views?.Get(summary.DestinationCondition),
                    setZeroWhenOutOfCondition: summary.SetZeroWhenOutOfCondition == true,
                    sourceSiteId: SiteId,
                    sourceReferenceType: "Results",
                    linkColumn: summary.LinkColumn,
                    type: summary.Type,
                    sourceColumn: summary.SourceColumn,
                    sourceCondition: ss.Views?.Get(summary.SourceCondition),
                    id: id);
            }
        }

        private void SynchronizeSourceSummary(
            Context context, SiteSettings ss, bool force = false)
        {
            ss.Sources.Values.ForEach(sourceSs =>
                sourceSs.Summaries
                    .Where(o => ss.Views?.Get(o.DestinationCondition) != null || force)
                    .ForEach(summary =>
                        Summaries.Synchronize(
                            context: context,
                            ss: sourceSs,
                            destinationSs: ss,
                            destinationSiteId: summary.SiteId,
                            destinationColumn: summary.DestinationColumn,
                            destinationCondition: ss.Views?.Get(summary.DestinationCondition),
                            setZeroWhenOutOfCondition: summary.SetZeroWhenOutOfCondition == true,
                            sourceSiteId: sourceSs.SiteId,
                            sourceReferenceType: sourceSs.ReferenceType,
                            linkColumn: summary.LinkColumn,
                            type: summary.Type,
                            sourceColumn: summary.SourceColumn,
                            sourceCondition: sourceSs.Views?.Get(summary.SourceCondition),
                            id: ResultId)));
        }

        private long SynchronizeSummaryDestinationId(string linkColumn, bool saved = false)
        {
            return saved
                ? GetSavedClass(linkColumn).ToLong()
                : GetClass(linkColumn).ToLong();
        }

        public void UpdateFormulaColumns(
            Context context, SiteSettings ss, IEnumerable<int> selected = null)
        {
            SetByFormula(context: context, ss: ss);
            var param = Rds.ResultsParam();
            ss.Formulas?
                .Where(o => selected == null || selected.Contains(o.Id))
                .ForEach(formulaSet =>
                {
                    if (string.IsNullOrEmpty(formulaSet.CalculationMethod)
                        || formulaSet.CalculationMethod == FormulaSet.CalculationMethods.Default.ToString())
                    {
                        switch (formulaSet.Target)
                        {
                            default:
                                if (Def.ExtendedColumnTypes.ContainsKey(formulaSet.Target ?? string.Empty))
                                {
                                    param.Add(
                                        columnBracket: $"\"{formulaSet.Target}\"",
                                        name: formulaSet.Target,
                                        value: GetNum(formulaSet.Target).Value);
                                }
                                break;
                        }
                    }
                    else if (formulaSet.CalculationMethod == FormulaSet.CalculationMethods.Extended.ToString())
                    {
                        switch (formulaSet.Target)
                        {
                            case "Title": param.Title(Title.Value); break;
                            case "Body": param.Body(Body); break;
                            case "Status": param.Status(Status.Value); break;
                            case "Manager": param.Manager(Manager.Id); break;
                            case "Owner": param.Owner(Owner.Id); break;
                            case "Locked": param.Locked(Locked); break;
                                case "Comments": param.Comments(Comments.ToString()); break;
                            default:
                                if (Def.ExtendedColumnTypes.ContainsKey(formulaSet.Target ?? string.Empty))
                                {
                                    switch (Def.ExtendedColumnTypes.Get(formulaSet.Target))
                                    {
                                        case "Class":
                                            param.Add(
                                                columnBracket: $"\"{formulaSet.Target}\"",
                                                name: formulaSet.Target,
                                                value: GetClass(formulaSet.Target));
                                            break;
                                        case "Num":
                                            param.Add(
                                                columnBracket: $"\"{formulaSet.Target}\"",
                                                name: formulaSet.Target,
                                                value: GetNum(formulaSet.Target).Value);
                                            break;
                                        case "Date":
                                            param.Add(
                                                columnBracket: $"\"{formulaSet.Target}\"",
                                                name: formulaSet.Target,
                                                value: GetDate(formulaSet.Target));
                                            break;
                                        case "Description":
                                            param.Add(
                                                columnBracket: $"\"{formulaSet.Target}\"",
                                                name: formulaSet.Target,
                                                value: GetDescription(formulaSet.Target));
                                            break;
                                        case "Check":
                                            param.Add(
                                                columnBracket: $"\"{formulaSet.Target}\"",
                                                name: formulaSet.Target,
                                                value: GetCheck(formulaSet.Target));
                                            break;
                                    }
                                    break;
                                }
                                break;
                        }
                    }
                });
            var paramFilter = param.Where(p => p.Value != null).ToList();
            if (paramFilter.Count > 0)
            {
                Repository.ExecuteNonQuery(
                    context: context,
                    statements: Rds.UpdateResults(
                        param: param,
                        where: Rds.ResultsWhereDefault(
                            context: context,
                            resultModel: this),
                        addUpdatedTimeParam: false,
                        addUpdatorParam: false));
            }
        }

        public void SetByFormula(Context context, SiteSettings ss)
        {
            SetByBeforeFormulaServerScript(
                context: context,
                ss: ss);
            ss.Formulas?.ForEach(formulaSet =>
            {
                var columnName = formulaSet.Target;
                var view = ss.Views?.Get(formulaSet.Condition);
                var isOutOfCondition = view != null && !Matched(context: context, ss: ss, view: view);
                if (string.IsNullOrEmpty(formulaSet.CalculationMethod)
                    || formulaSet.CalculationMethod == FormulaSet.CalculationMethods.Default.ToString())
                {
                    var formula = formulaSet.Formula;
                    if (isOutOfCondition)
                    {
                        if (formulaSet.OutOfCondition != null)
                        {
                            formula = formulaSet.OutOfCondition;
                        }
                        else
                        {
                            return;
                        }
                    }
                    var data = new Dictionary<string, decimal>
                    {
                    };
                    data.AddRange(NumHash.ToDictionary(
                        o => o.Key,
                        o => o.Value?.Value?.ToDecimal() ?? 0));
                    var value = formula?.GetResult(
                        data: data,
                        column: ss.GetColumn(
                            context: context,
                            columnName: columnName)) ?? 0;
                    switch (columnName)
                    {
                        default:
                            SetNum(
                                columnName: columnName,
                                value: new Num(value));
                            break;
                    }
                    if (ss.OutputFormulaLogs == true)
                    {
                        context.LogBuilder?.AppendLine($"formulaSet: {formulaSet.GetRecordingData().ToJson()}");
                        context.LogBuilder?.AppendLine($"formulaSource: {data.ToJson()}");
                        context.LogBuilder?.AppendLine($"formulaResult: {{\"{columnName}\":{value}}}");
                    }
                }
                else if (formulaSet.CalculationMethod == FormulaSet.CalculationMethods.Extended.ToString())
                {
                    var formula = formulaSet.Formula;
                    if (isOutOfCondition && formulaSet.FormulaScriptOutOfCondition == null)
                    {
                        return;
                    }
                    var value = ExecFormulaExtended(
                        context: context,
                        ss: ss,
                        columnName: columnName,
                        formulaSet: formulaSet,
                        isOutOfCondition: isOutOfCondition,
                        outputFormulaLogs: ss.OutputFormulaLogs);
                    var formData =  new Dictionary<string, string>
                    {
                        { $"Results_{columnName}", value }
                    };
                    SetByFormData(
                        context: context,
                        ss: ss,
                        formData: formData);
                }
            });
            SetByAfterFormulaServerScript(
                context: context,
                ss: ss);
        }

        private string ExecFormulaExtended(
            Context context,
            SiteSettings ss,
            string columnName,
            FormulaSet formulaSet,
            bool isOutOfCondition,
            bool? outputFormulaLogs)
        {
            var script = isOutOfCondition == false
                ? formulaSet.FormulaScript
                : formulaSet.FormulaScriptOutOfCondition;
            if (script == null) script = string.Empty;
            SetExtendedColumnDefaultValue(
                ss: ss,
                formulaScript: script,
                calculationMethod: formulaSet.CalculationMethod);
            formulaSet = FormulaBuilder.UpdateColumnDisplayText(
                ss: ss,
                formulaSet: formulaSet);
            script = FormulaBuilder.ParseFormulaScript(
                ss: ss,
                formulaScript: formulaSet.FormulaScript,
                calculationMethod: formulaSet.CalculationMethod);
            var value = FormulaServerScriptUtilities.Execute(
                context: context,
                ss: ss,
                itemModel: this,
                formulaScript: script);
            switch (value)
            {
                case "#N/A":
                case "#VALUE!":
                case "#REF!":
                case "#DIV/0!":
                case "#NUM!":
                case "#NAME?":
                case "#NULL!":
                case "Invalid Parameter":
                    if (formulaSet.IsDisplayError == true)
                    {
                        throw new Exception($"Formula error {value}");
                    }
                    new SysLogModel(
                        context: context,
                        method: nameof(SetByFormula),
                        message: $"Formula error {value}",
                        sysLogType: SysLogModel.SysLogTypes.Exception);
                    break;
            }
            if (outputFormulaLogs == true)
            {
                context.LogBuilder?.AppendLine($"formulaSet: {formulaSet.GetRecordingData().ToJson()}");
                context.LogBuilder?.AppendLine($"formulaSource: {this.ToJson()}");
                context.LogBuilder?.AppendLine($"formulaResult: {{\"{columnName}\":{value}}}");
            }
            return value.ToString();
        }

        public void SetTitle(Context context, SiteSettings ss)
        {
            if (Title?.ItemTitle != true)
            {
                Title = new Title(
                    context: context,
                    ss: ss,
                    id: ResultId,
                    ver: Ver,
                    isHistory: VerType == Versions.VerTypes.History,
                    data: PropertyValues(
                        context: context,
                        columns: ss.GetTitleColumns(context: context)));
            }
        }

        private bool Matched(Context context, SiteSettings ss, View view)
        {
            var userId = context.UserId;
            if (view.Own == true && !(Manager.Id == userId || Owner.Id == userId))
            {
                return false;
            }
            if (view.ColumnFilterHash != null)
            {
                foreach (var filter in view.ColumnFilterHash)
                {
                    var match = true;
                    var column = ss.GetColumn(context: context, columnName: filter.Key);
                    switch (filter.Key)
                    {
                        case "UpdatedTime":
                            match = UpdatedTime?.Value.Matched(
                                context: context,
                                column: column,
                                condition: filter.Value) == true;
                            break;
                        case "ResultId":
                            match = ResultId.Matched(
                                column: column,
                                condition: filter.Value);
                            break;
                        case "Ver":
                            match = Ver.Matched(
                                context: context,
                                column: column,
                                condition: filter.Value);
                            break;
                        case "Title":
                            match = Title.Value.Matched(
                                context: context,
                                column: column,
                                condition: filter.Value);
                            break;
                        case "Body":
                            match = Body.Matched(
                                context: context,
                                column: column,
                                condition: filter.Value);
                            break;
                        case "Status":
                            match = Status.Value.Matched(
                                context: context,
                                column: column,
                                condition: filter.Value);
                            break;
                        case "Locked":
                            match = Locked.Matched(
                                column: column,
                                condition: filter.Value);
                            break;
                        case "SiteTitle":
                            match = SiteTitle.SiteId.Matched(
                                column: column,
                                condition: filter.Value);
                            break;
                        case "Creator":
                            match = Creator.Id.Matched(
                                context: context,
                                column: column,
                                condition: filter.Value);
                            break;
                        case "Updator":
                            match = Updator.Id.Matched(
                                context: context,
                                column: column,
                                condition: filter.Value);
                            break;
                        case "CreatedTime":
                            match = CreatedTime?.Value.Matched(
                                context: context,
                                column: column,
                                condition: filter.Value) == true;
                            break;
                        case "Manager":
                            if (Manager.Id == 0 && filter.Value == "[\"\\t\"]")
                            {
                                match = true;
                            } else
                            {
                                match = Manager.Id.Matched(
                                    context: context,
                                    column: column,
                                    condition: filter.Value);
                            }
                            break;
                        case "Owner":
                            if (Owner.Id == 0 && filter.Value == "[\"\\t\"]")
                            {
                                match = true;
                            } else
                            {
                                match = Owner.Id.Matched(
                                    context: context,
                                    column: column,
                                    condition: filter.Value);
                            }
                            break;
                        default:
                            switch (Def.ExtendedColumnTypes.Get(filter.Key ?? string.Empty))
                            {
                                case "Class":
                                    match = GetClass(column: column).Matched(
                                        context: context,
                                        column: column,
                                        condition: filter.Value);
                                    break;
                                case "Num":
                                    match = GetNum(column: column).Matched(
                                        column: column,
                                        condition: filter.Value);
                                    break;
                                case "Date":
                                    match = GetDate(column: column).Matched(
                                        context: context,
                                        column: column,
                                        condition: filter.Value);
                                    break;
                                case "Description":
                                    match = GetDescription(column: column).Matched(
                                        context: context,
                                        column: column,
                                        condition: filter.Value);
                                    break;
                                case "Check":
                                    match = GetCheck(column: column).Matched(
                                        column: column,
                                        condition: filter.Value);
                                    break;
                            }
                            break;
                    }
                    if (!match) return false;
                }
            }
            return true;
        }

        public string ReplacedDisplayValues(
            Context context,
            SiteSettings ss,
            string value)
        {
            ss.IncludedColumns(value: value).ForEach(column =>
                value = value.Replace(
                    $"[{column.ColumnName}]",
                    ToDisplay(
                        context: context,
                        ss: ss,
                        column: column,
                        mine: Mine(context: context))));
            value = ReplacedContextValues(context, value);
            return value;
        }

        private string ReplacedContextValues(Context context, string value)
        {
            var url = Locations.ItemEditAbsoluteUri(
                context: context,
                id: ResultId);
            var mailAddress = MailAddressUtilities.Get(
                context: context,
                userId: context.UserId);
            value = value
                .Replace("{Url}", url)
                .Replace("{LoginId}", context.User.LoginId)
                .Replace("{UserName}", context.User.Name)
                .Replace("{MailAddress}", mailAddress);
            return value;
        }

        public List<Notification> GetNotifications(
            Context context,
            SiteSettings ss,
            bool notice,
            bool before = false,
            Sqls.TableTypes tableTypes = Sqls.TableTypes.Normal)
        {
            if (context.ContractSettings.Notice == false || !notice)
            {
                return null;
            }
            var notifications = NotificationUtilities.Get(
                context: context,
                ss: ss);
            if (notifications?.Any() == true)
            {
                var dataSet = Repository.ExecuteDataSet(
                    context: context,
                    statements: notifications.Select(notification =>
                    {
                        var where = ss.Views?.Get(before
                            ? notification.BeforeCondition
                            : notification.AfterCondition)
                                ?.Where(
                                    context: context,
                                    ss: ss,
                                    where: Rds.ResultsWhere().ResultId(ResultId))
                                        ?? Rds.ResultsWhere().ResultId(ResultId);
                        return Rds.SelectResults(
                            dataTableName: notification.Index.ToString(),
                            tableType: tableTypes,
                            column: Rds.ResultsColumn().ResultId(),
                            join: ss.Join(
                                context: context,
                                join: where),
                            where: where);
                    }).ToArray());
                return notifications
                    .Where(notification =>
                        dataSet.Tables[notification.Index.ToString()].Rows.Count == 1)
                    .ToList();
            }
            else
            {
                return null;
            }
        }

        public void Notice(
            Context context,
            SiteSettings ss,
            List<Notification> notifications,
            string type)
        {
            notifications?.ForEach(notification =>
            {
                if (notification.HasRelatedUsers())
                {
                    var users = new List<int>();
                    Repository.ExecuteTable(
                        context: context,
                        statements: Rds.SelectResults(
                            tableType: Sqls.TableTypes.All,
                            distinct: true,
                            column: Rds.ResultsColumn()
                                .Manager()
                                .Owner()
                                .Creator()
                                .Updator(),
                            where: Rds.ResultsWhere().ResultId(ResultId)))
                                .AsEnumerable()
                                .ForEach(dataRow =>
                                {
                                    users.Add(dataRow.Int("Manager"));
                                    users.Add(dataRow.Int("Owner"));
                                    users.Add(dataRow.Int("Creator"));
                                    users.Add(dataRow.Int("Updator"));
                                });
                    notification.ReplaceRelatedUsers(
                        context: context,
                        users: users);
                }
                var valuesTo = ss.IncludedColumns(notification.Address)
                    .ToDictionary(
                        column => column,
                        column => PropertyValue(
                            context: context,
                            column: column));
                var valuesCc = ss.IncludedColumns(notification.CcAddress)
                    .ToDictionary(
                        column => column,
                        column => PropertyValue(
                            context: context,
                            column: column));
                var valuesBcc = ss.IncludedColumns(notification.BccAddress)
                    .ToDictionary(
                        column => column,
                        column => PropertyValue(
                            context: context,
                            column: column));
                switch (type)
                {
                    case "Created":
                    case "Copied":
                        if ((type == "Created" && notification.AfterCreate != false)
                            || (type == "Copied" && notification.AfterCopy != false))
                        {
                            notification.Send(
                                context: context,
                                ss: ss,
                                title: notification.Subject.IsNullOrEmpty()
                                    ? Displays.Created(
                                        context: context,
                                        data: Title.DisplayValue).ToString()
                                    : ReplacedDisplayValues(
                                        context: context,
                                        ss: ss,
                                        value: notification.Subject.Replace(
                                            "[NotificationTrigger]",
                                            Displays.CreatedWord(context: context))),
                                body: NoticeBody(
                                    context: context,
                                    ss: ss,
                                    notification: notification),
                                valuesTo: valuesTo,
                                valuesCc: valuesCc,
                                valuesBcc: valuesBcc);
                        }
                        break;
                    case "Updated":
                        if (notification.AfterUpdate != false
                            && notification.MonitorChangesColumns.Any(columnName => PropertyUpdated(
                                context: context,
                                name: columnName)))
                        {
                            var body = NoticeBody(
                                context: context,
                                ss: ss,
                                notification: notification,
                                update: true);
                            notification.Send(
                                context: context,
                                ss: ss,
                                title: notification.Subject.IsNullOrEmpty()
                                    ? Displays.Updated(
                                        context: context,
                                        data: Title.DisplayValue).ToString()
                                    : ReplacedDisplayValues(
                                        context: context,
                                        ss: ss,
                                        value: notification.Subject.Replace(
                                            "[NotificationTrigger]",
                                            Displays.UpdatedWord(context: context))),
                                body: body,
                                valuesTo: valuesTo,
                                valuesCc: valuesCc,
                                valuesBcc: valuesBcc);
                        }
                        break;
                    case "Deleted":
                        if (notification.AfterDelete != false)
                        {
                            notification.Send(
                                context: context,
                                ss: ss,
                                title: notification.Subject.IsNullOrEmpty()
                                    ? Displays.Deleted(
                                        context: context,
                                        data: Title.DisplayValue).ToString()
                                    : ReplacedDisplayValues(
                                        context: context,
                                        ss: ss,
                                        value: notification.Subject.Replace(
                                            "[NotificationTrigger]",
                                            Displays.DeletedWord(context: context))),
                                body: NoticeBody(
                                    context: context,
                                    ss: ss,
                                    notification: notification),
                                valuesTo: valuesTo,
                                valuesCc: valuesCc,
                                valuesBcc: valuesBcc);
                        }
                        break;
                }
            });
        }

        private string NoticeBody(
            Context context,
            SiteSettings ss,
            Notification notification,
            bool update = false)
        {
            var body = new System.Text.StringBuilder();
            notification.GetFormat(
                context: context,
                ss: ss)
                    .Split('\n')
                    .Select(line => new
                    {
                        Line = line.Trim(),
                        Format = line.Trim().Deserialize<NotificationColumnFormat>()
                    })
                    .ForEach(data =>
                    {
                        var column = ss.IncludedColumns(data.Format?.Name)?.FirstOrDefault();
                        if (column == null)
                        {
                            body.Append(ReplacedContextValues(
                                context: context,
                                value: data.Line));
                            body.Append("\n");
                        }
                        else
                        {
                            switch (column.Name)
                            {
                                case "Title":
                                    body.Append(Title.ToNotice(
                                        context: context,
                                        saved: SavedTitle,
                                        column: column,
                                        notificationColumnFormat: data.Format,
                                        updated: Title_Updated(context: context),
                                        update: update));
                                    break;
                                case "Body":
                                    body.Append(Body.ToNotice(
                                        context: context,
                                        saved: SavedBody,
                                        column: column,
                                        notificationColumnFormat: data.Format,
                                        updated: Body_Updated(context: context),
                                        update: update));
                                    break;
                                case "Status":
                                    body.Append(Status.ToNotice(
                                        context: context,
                                        saved: SavedStatus,
                                        column: column,
                                        notificationColumnFormat: data.Format,
                                        updated: Status_Updated(context: context),
                                        update: update));
                                    break;
                                case "Manager":
                                    body.Append(Manager.ToNotice(
                                        context: context,
                                        saved: SavedManager,
                                        column: column,
                                        notificationColumnFormat: data.Format,
                                        updated: Manager_Updated(context: context),
                                        update: update));
                                    break;
                                case "Owner":
                                    body.Append(Owner.ToNotice(
                                        context: context,
                                        saved: SavedOwner,
                                        column: column,
                                        notificationColumnFormat: data.Format,
                                        updated: Owner_Updated(context: context),
                                        update: update));
                                    break;
                                case "Locked":
                                    body.Append(Locked.ToNotice(
                                        context: context,
                                        saved: SavedLocked,
                                        column: column,
                                        notificationColumnFormat: data.Format,
                                        updated: Locked_Updated(context: context),
                                        update: update));
                                    break;
                                case "Comments":
                                    body.Append(Comments.ToNotice(
                                        context: context,
                                        saved: SavedComments,
                                        column: column,
                                        notificationColumnFormat: data.Format,
                                        updated: Comments_Updated(context: context),
                                        update: update));
                                    break;
                                case "Creator":
                                    body.Append(Creator.ToNotice(
                                        context: context,
                                        saved: SavedCreator,
                                        column: column,
                                        notificationColumnFormat: data.Format,
                                        updated: Creator_Updated(context: context),
                                        update: update));
                                    break;
                                case "Updator":
                                    body.Append(Updator.ToNotice(
                                        context: context,
                                        saved: SavedUpdator,
                                        column: column,
                                        notificationColumnFormat: data.Format,
                                        updated: Updator_Updated(context: context),
                                        update: update));
                                    break;
                                default:
                                    switch (Def.ExtendedColumnTypes.Get(column?.Name ?? string.Empty))
                                    {
                                        case "Class":
                                            body.Append(GetClass(columnName: column.Name).ToNotice(
                                                context: context,
                                                saved: GetSavedClass(columnName: column.Name),
                                                column: column,
                                                notificationColumnFormat: data.Format,
                                                updated: Class_Updated(columnName: column.Name),
                                                update: update));
                                            break;
                                        case "Num":
                                            body.Append(GetNum(columnName: column.Name).ToNotice(
                                                context: context,
                                                saved: GetSavedNum(columnName: column.Name),
                                                column: column,
                                                notificationColumnFormat: data.Format,
                                                updated: Num_Updated(columnName: column.Name),
                                                update: update));
                                            break;
                                        case "Date":
                                            body.Append(GetDate(columnName: column.Name).ToNotice(
                                                context: context,
                                                saved: GetSavedDate(columnName: column.Name),
                                                column: column,
                                                notificationColumnFormat: data.Format,
                                                updated: Date_Updated(columnName: column.Name),
                                                update: update));
                                            break;
                                        case "Description":
                                            body.Append(GetDescription(columnName: column.Name).ToNotice(
                                                context: context,
                                                saved: GetSavedDescription(columnName: column.Name),
                                                column: column,
                                                notificationColumnFormat: data.Format,
                                                updated: Description_Updated(columnName: column.Name),
                                                update: update));
                                            break;
                                        case "Check":
                                            body.Append(GetCheck(columnName: column.Name).ToNotice(
                                                context: context,
                                                saved: GetSavedCheck(columnName: column.Name),
                                                column: column,
                                                notificationColumnFormat: data.Format,
                                                updated: Check_Updated(columnName: column.Name),
                                                update: update));
                                            break;
                                        case "Attachments":
                                            body.Append(GetAttachments(columnName: column.Name).ToNotice(
                                                context: context,
                                                saved: GetSavedAttachments(columnName: column.Name),
                                                column: column,
                                                notificationColumnFormat: data.Format,
                                                updated: Attachments_Updated(columnName: column.Name),
                                                update: update));
                                            break;
                                    }
                                    break;
                            }
                        }
                    });
            return body.ToString();
        }

        private void SetBySession(Context context)
        {
        }

        private void Set(Context context, SiteSettings ss, DataTable dataTable)
        {
            switch (dataTable.Rows.Count)
            {
                case 1: Set(context, ss, dataTable.Rows[0]); break;
                case 0: AccessStatus = Databases.AccessStatuses.NotFound; break;
                default: AccessStatus = Databases.AccessStatuses.Overlap; break;
            }
            SetChoiceHash(context: context, ss: ss);
        }

        public void SetChoiceHash(Context context, SiteSettings ss)
        {
            if (!ss.SetAllChoices)
            {
                ss.GetUseSearchLinks(context: context).ForEach(link =>
                {
                    var column = ss.GetColumn(
                        context: context,
                        columnName: link.ColumnName);
                    var value = PropertyValue(
                        context: context,
                        column: column);
                    if (!value.IsNullOrEmpty() 
                        && column?.ChoiceHash?.Any(o => o.Value.Value == value) != true)
                    {
                        ss.SetChoiceHash(
                            context: context,
                            columnName: column.ColumnName,
                            selectedValues: value.ToSingleList());
                    }
                });
            }
            SetTitle(context: context, ss: ss);
        }

        private void Set(Context context, SiteSettings ss, DataRow dataRow, string tableAlias = null)
        {
            AccessStatus = Databases.AccessStatuses.Selected;
            foreach (DataColumn dataColumn in dataRow.Table.Columns)
            {
                var column = new ColumnNameInfo(dataColumn.ColumnName);
                if (column.TableAlias == tableAlias)
                {
                    switch (column.Name)
                    {
                        case "SiteId":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                SiteId = dataRow[column.ColumnName].ToLong();
                                SavedSiteId = SiteId;
                            }
                            break;
                        case "UpdatedTime":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                UpdatedTime = new Time(context, dataRow, column.ColumnName); Timestamp = dataRow.Field<DateTime>(column.ColumnName).ToString("yyyy/M/d H:m:s.fff");
                                SavedUpdatedTime = UpdatedTime.Value;
                            }
                            break;
                        case "ResultId":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                ResultId = dataRow[column.ColumnName].ToLong();
                                SavedResultId = ResultId;
                            }
                            break;
                        case "Ver":
                            Ver = dataRow[column.ColumnName].ToInt();
                            SavedVer = Ver;
                            break;
                        case "Title":
                            Title = new Title(context: context, ss: ss, dataRow: dataRow, column: column);
                            SavedTitle = Title.Value;
                            break;
                        case "Body":
                            Body = dataRow[column.ColumnName].ToString();
                            SavedBody = Body;
                            break;
                        case "Status":
                            Status = new Status(dataRow, column);
                            SavedStatus = Status.Value;
                            break;
                        case "Manager":
                            Manager = SiteInfo.User(context: context, userId: dataRow.Int(column.ColumnName));
                            SavedManager = Manager.Id;
                            break;
                        case "Owner":
                            Owner = SiteInfo.User(context: context, userId: dataRow.Int(column.ColumnName));
                            SavedOwner = Owner.Id;
                            break;
                        case "Locked":
                            Locked = dataRow[column.ColumnName].ToBool();
                            SavedLocked = Locked;
                            break;
                        case "Comments":
                            Comments = dataRow[column.ColumnName].ToString().Deserialize<Comments>() ?? new Comments();
                            SavedComments = Comments.ToJson();
                            break;
                        case "Creator":
                            Creator = SiteInfo.User(context: context, userId: dataRow.Int(column.ColumnName));
                            SavedCreator = Creator.Id;
                            break;
                        case "Updator":
                            Updator = SiteInfo.User(context: context, userId: dataRow.Int(column.ColumnName));
                            SavedUpdator = Updator.Id;
                            break;
                        case "CreatedTime":
                            CreatedTime = new Time(context, dataRow, column.ColumnName);
                            SavedCreatedTime = CreatedTime.Value;
                            break;
                        case "IsHistory":
                            VerType = dataRow.Bool(column.ColumnName)
                                ? Versions.VerTypes.History
                                : Versions.VerTypes.Latest; break;
                        default:
                            switch (Def.ExtendedColumnTypes.Get(column?.Name ?? string.Empty))
                            {
                                case "Class":
                                    SetClass(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToString());
                                    SetSavedClass(
                                        columnName: column.Name,
                                        value: GetClass(columnName: column.Name));
                                    break;
                                case "Num":
                                    SetNum(
                                        columnName: column.Name,
                                        value: new Num(
                                            dataRow: dataRow,
                                            name: column.ColumnName));
                                    SetSavedNum(
                                        columnName: column.Name,
                                        value: GetNum(columnName: column.Name).Value);
                                    break;
                                case "Date":
                                    SetDate(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToDateTime());
                                    SetSavedDate(
                                        columnName: column.Name,
                                        value: GetDate(columnName: column.Name));
                                    break;
                                case "Description":
                                    SetDescription(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToString());
                                    SetSavedDescription(
                                        columnName: column.Name,
                                        value: GetDescription(columnName: column.Name));
                                    break;
                                case "Check":
                                    SetCheck(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToBool());
                                    SetSavedCheck(
                                        columnName: column.Name,
                                        value: GetCheck(columnName: column.Name));
                                    break;
                                case "Attachments":
                                    SetAttachments(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToString()
                                            .Deserialize<Attachments>() ?? new Attachments());
                                    SetSavedAttachments(
                                        columnName: column.Name,
                                        value: GetAttachments(columnName: column.Name).ToJson());
                                    break;
                            }
                            break;
                    }
                }
            }
            SetTitle(context: context, ss: ss);
            SetByWhenloadingRecordServerScript(
                context: context,
                ss: ss);
        }

        public bool Updated(Context context)
        {
            return Updated()
                || SiteId_Updated(context: context)
                || Ver_Updated(context: context)
                || Title_Updated(context: context)
                || Body_Updated(context: context)
                || Status_Updated(context: context)
                || Manager_Updated(context: context)
                || Owner_Updated(context: context)
                || Locked_Updated(context: context)
                || Comments_Updated(context: context)
                || Creator_Updated(context: context)
                || Updator_Updated(context: context);
        }

        private bool UpdatedWithColumn(Context context, SiteSettings ss)
        {
            return ClassHash.Any(o => Class_Updated(
                    columnName: o.Key,
                    column: ss.GetColumn(context: context, o.Key)))
                || NumHash.Any(o => Num_Updated(
                    columnName: o.Key,
                    column: ss.GetColumn(context: context, o.Key)))
                || DateHash.Any(o => Date_Updated(
                    columnName: o.Key,
                    column: ss.GetColumn(context: context, o.Key)))
                || DescriptionHash.Any(o => Description_Updated(
                    columnName: o.Key,
                    column: ss.GetColumn(context: context, o.Key)))
                || CheckHash.Any(o => Check_Updated(
                    columnName: o.Key,
                    column: ss.GetColumn(context: context, o.Key)))
                || AttachmentsHash.Any(o => Attachments_Updated(
                    columnName: o.Key,
                    column: ss.GetColumn(context: context, o.Key)));
        }

        public bool Updated(Context context, SiteSettings ss)
        {
            return UpdatedWithColumn(context: context, ss: ss)
                || SiteId_Updated(context: context)
                || Ver_Updated(context: context)
                || Title_Updated(context: context)
                || Body_Updated(context: context)
                || Status_Updated(context: context)
                || Manager_Updated(context: context)
                || Owner_Updated(context: context)
                || Locked_Updated(context: context)
                || Comments_Updated(context: context)
                || Creator_Updated(context: context)
                || Updator_Updated(context: context);
        }

        public override List<string> Mine(Context context)
        {
            if (MineCache == null)
            {
                var mine = new List<string>();
                var userId = context.UserId;
                if (SavedManager == userId) mine.Add("Manager");
                if (SavedOwner == userId) mine.Add("Owner");
                if (SavedCreator == userId) mine.Add("Creator");
                if (SavedUpdator == userId) mine.Add("Updator");
                MineCache = mine;
            }
            return MineCache;
        }

        public string IdSuffix()
        {
            return $"_{SiteId}_{(ResultId == 0 ? -1 : ResultId)}";
        }
    }
}
