﻿using System;
using System.Collections.Generic;
using System.Text;
using GSM05000Common.DTO;
using R_CommonFrontBackAPI;

namespace GSM05000Common.Interface
{
    public interface IGSM05000Transaction : R_IServiceCRUDBase<GSM05000TransactionDetailDTO>
    {
        IAsyncEnumerable<GSM05000TransactionDTO> GetTransactionCodeListStream();
        GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList();
        GSM05000ExistDTO CheckExistData(GSM05000TrxCodeParamsDTO poParams);
    }

    public interface IGSM05000ApprovalUser : R_IServiceCRUDBase<GSM05000ApprovalUserDTO>
    {
        GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader(GSM05000TrxCodeParamsDTO poParams);
        IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetApprovalListStream();
        string GSM05000ValidationForAction(GSM05000TrxDeptParamsDTO poParams);
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartmentStream();
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000DepartmentChangeSequenceStream(GSM05000TrxCodeParamsDTO poParams);
        IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetUserSequenceDataStream();
        void GSM05000UpdateSequence(List<GSM05000ApprovalUserDTO> poEntity);
        // IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartmentStream(GSM05000DeptCodeParamsDTO poParams);
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartmentStream();
        GSM05000SingleDTO<string> GSM05000CopyToApproval(GSM05000CopyToParamsDTO poParams);
        GSM05000SingleDTO<string> GSM05000CopyFromApproval(GSM05000CopyFromParamsDTO poParams);
    }

    public interface IGSM05000ApprovalReplacement : R_IServiceCRUDBase<GSM05000ApprovalReplacementDTO>
    {
        IAsyncEnumerable<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacementListStream();
    }
}
