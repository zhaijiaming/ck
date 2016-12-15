using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKWMS.BSL;
using CKWMS.EFModels;
using CKWMS.BSL;
using CKWMS.IBSL;
using CKWMS.App_Code;
using System.Web.Mvc;
using CKWMS.Common;
using System.Linq.Expressions;

namespace CKWMS.reports
{
    public partial class ReportZY : System.Web.UI.Page
    {
        private Iwms_jianhuoService ob_wms_jianhuoservice = ServiceFactory.wms_jianhuoservice;
        private Iwms_chukumxService ob_wms_chukumxservice = ServiceFactory.wms_chukumxservice;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int _userid = (int)Session["user_id"];
                string name = "";
                int i = 0;
                string _outid = "";
                string _rkysid = "";
                string _rkxdid = "";
                string _sjdid = "";
                string _rkmxid = "";
                string _zlgl_rkysid = "";
                string _zlgl_ckfhid = "";
                string _spxx_huozhuid = "";
                string _JH_ckjyid = "";
                ReportDataSetZY _rds = new ReportDataSetZY();
                //DataTable _dt;                
                if (Request.QueryString["pid"] != null)
                {
                    name = Request.QueryString["pid"];
                    _outid = Request.QueryString["out"] ?? "";
                    _rkysid = Request.QueryString["rkysid"] ?? "";
                    _rkxdid = Request.QueryString["rkxdid"] ?? "";
                    _sjdid = Request.QueryString["sjdid"] ?? "";
                    _rkmxid = Request.QueryString["rkmxid"] ?? "";
                    _zlgl_rkysid = Request.QueryString["zlgl_rkysid"] ?? "";
                    _zlgl_ckfhid = Request.QueryString["zlgl_ckfhid"] ?? "";
                    _spxx_huozhuid = Request.QueryString["spxx_huozhuid"] ?? "";
                    _JH_ckjyid = Request.QueryString["JH_ckjyid"] ?? "";
                    switch (name)
                    {
                        case "daviskw":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/daviskw.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtkw = _rds.Tables["davistest-kw"];
                            var _kws = ServiceFactory.wms_kuweiservice.LoadEntities(p => p.IsDelete == false);
                            DataRow drkw;
                            foreach (wms_kuwei _pr in _kws)
                            {
                                drkw = dtkw.NewRow();
                                drkw["kuwei"] = _pr.Mingcheng;
                                drkw["huojia"] = _pr.Huojia;
                                drkw["lei"] = _pr.Lieshu;
                                drkw["ceng"] = _pr.Censhu;
                                dtkw.Rows.Add(drkw);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["davistest-kw"]));
                            break;
                        case "JianHuoDan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptJianHuoDan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtjhd = _rds.Tables["JianHuoDan"];
                            DataRow drjhd;
                            long jhd_DaijianSLs = 0;
                            try
                            {
                                var _jhds = ob_wms_jianhuoservice.GetPickDetail(int.Parse(_outid), p => p.DaijianSL > 0).OrderBy(p => p.Kuwei).ThenBy(p => p.Pihao).ToList<wms_pick_v>();
                                foreach (wms_pick_v _pv in _jhds)
                                {
                                    drjhd = dtjhd.NewRow();
                                    drjhd["Mingcheng"] = _pv.ShangpinMC;
                                    drjhd["DaijianSL"] = _pv.DaijianSL;
                                    drjhd["Huansuanlv"] = _pv.Huansuanlv;
                                    drjhd["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pv.ShixiaoRQ == null ? "" : ((DateTime)_pv.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drjhd["Guige"] = _pv.Guige;
                                    drjhd["Pihao"] = _pv.Pihao;
                                    drjhd["Kuwei"] = _pv.Kuwei;
                                    drjhd["Zhucezheng"] = _pv.Zhucezheng;
                                    drjhd["JianhuoSM"] = _pv.JianhuoSM;
                                    jhd_DaijianSLs += (long)_pv.DaijianSL;

                                    dtjhd.Rows.Add(drjhd);
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["JianHuoDan"]));

                            DataTable dtjhdt = _rds.Tables["JianHuoDan_Title"];
                            DataRow drjhdt = dtjhdt.NewRow();
                            try
                            {
                                wms_chukudan tempdata = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (tempdata != null)
                                {
                                    drjhdt["Yunsongdizhi"] = tempdata.Yunsongdizhi;
                                    drjhdt["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", tempdata.ChukuRQ == null ? "" : ((DateTime)tempdata.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drjhdt["ChukudanBH"] = tempdata.ChukudanBH;
                                    drjhdt["Lianxiren"] = tempdata.Lianxiren;
                                    drjhdt["LianxiDH"] = tempdata.LianxiDH;
                                    drjhdt["Beizhu"] = tempdata.Beizhu;
                                    drjhdt["jhddjSLs"] = jhd_DaijianSLs;
                                    drjhdt["KehuMC"] = tempdata.KehuMC;
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == tempdata.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drjhdt["HuozhuID"] = wtkhdata.Kehumingcheng;
                                    }
                                    dtjhdt.Rows.Add(drjhdt);
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["JianHuoDan_Title"]));
                            break;
                        case "tongxingdan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptTongxing.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dttx = _rds.Tables["TongXingDan"];
                            DataRow drtx;
                            long tx_ChukuSL = 0;
                            try
                            {
                                var _ckmxs = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_outid) && p.IsDelete == false, false, p => p.Guige);
                                foreach (wms_chukumx _mx in _ckmxs)
                                {
                                    drtx = dttx.NewRow();
                                    drtx["Guige"] = _mx.Guige;
                                    drtx["Changjia"] = _mx.Changjia;
                                    drtx["ShangpinMC"] = _mx.ShangpinMC;
                                    drtx["Pihao"] = _mx.Pihao;
                                    drtx["Xuliema"] = _mx.Xuliema;
                                    drtx["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _mx.ShixiaoRQ == null ? "" : ((DateTime)_mx.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drtx["Zhucezheng"] = _mx.Zhucezheng;
                                    drtx["ChukuSL"] = _mx.ChukuSL;
                                    drtx["JibenDW"] = _mx.JibenDW;
                                    wms_chukudan _ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                    if (_ckd != null)
                                    {
                                        drtx["ChunyunYQ"] = _ckd.ChunyunYQ;
                                    }
                                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.Qiyemingcheng == _mx.Changjia && p.IsDelete == false);
                                    if (_scqy != null)
                                    {
                                        drtx["ShengchanxukeBH"] = _scqy.ShengchanxukeBH;
                                        drtx["BeianBH"] = _scqy.BeianBH;
                                    }
                                    tx_ChukuSL += (long)_mx.ChukuSL;

                                    dttx.Rows.Add(drtx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["TongXingDan"]));
                            
                            //页眉&页脚
                            DataTable dtckd = _rds.Tables["TongXingDan_Title"];
                            DataRow drckd = dtckd.NewRow();
                            try
                            {
                                wms_chukudan ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (ckd != null)
                                {
                                    drckd["ChukudanBH"] = ckd.ChukudanBH;
                                    drckd["KehuMC"] = ckd.KehuMC;
                                    drckd["Yunsongdizhi"] = ckd.Yunsongdizhi;
                                    drckd["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", ckd.ChukuRQ == null ? "" : ((DateTime)ckd.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drckd["Lianxiren"] = ckd.Lianxiren;
                                    drckd["LianxiDH"] = ckd.LianxiDH;
                                    drckd["Beizhu"] = ckd.Beizhu;
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == ckd.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drckd["HuozhuID"] = wtkhdata.Kehumingcheng;
                                    }
                                }
                                drckd["txckSLs"] = tx_ChukuSL;
                                dtckd.Rows.Add(drckd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["TongXingDan_Title"]));
                            break;
                        case "CKFuheBaoGaoDan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptCKFuheDan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtfh = _rds.Tables["CKFuheBaoGao"];
                            DataRow drfh;
                            long ckfhbg_JianhuoSLs = 0;
                            long ckfhbg_FuheSLs = 0;
                            try
                            {
                                var _fhs = ServiceFactory.quan_chukufhservice.GetOutcheckByCK(int.Parse(_outid), p => p.JianhuoSL > 0).ToList<quan_outcheck_v>();
                                foreach (quan_outcheck_v _fh in _fhs)
                                {
                                    drfh = dtfh.NewRow();
                                    drfh["ShangpinMC"] = _fh.ShangpinMC;
                                    drfh["Zhucezheng"] = _fh.Zhucezheng;
                                    drfh["Guige"] = _fh.Guige;
                                    drfh["Pihao"] = _fh.Pihao;
                                    drfh["Pihao1"] = _fh.Pihao1;
                                    drfh["Xuliema"] = _fh.Xuliema;
                                    drfh["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShengchanRQ == null ? "" : ((DateTime)_fh.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drfh["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShixiaoRQ == null ? "" : ((DateTime)_fh.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drfh["JianhuoSL"] = _fh.JianhuoSL;
                                    drfh["Changjia"] = _fh.Changjia;
                                    drfh["Chandi"] = _fh.Chandi;
                                    drfh["FuheSL"] = _fh.FuheSL == null ? float.Parse("0") : _fh.FuheSL;
                                    if (_fh.Fuhe == null)
                                    {
                                        drfh["Fuhe"] = "未复核";
                                    }
                                    else
                                    {
                                        drfh["Fuhe"] = MvcApplication.CheckResult[(int)_fh.Fuhe];
                                    }
                                    drfh["Fuheren"] = _fh.Fuheren == null ? "" : _fh.Fuheren;
                                    drfh["FuheSM"] = _fh.FuheSM == null ? "" : _fh.FuheSM;
                                    drfh["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _fh.MakeDate == null ? "" : ((DateTime)_fh.MakeDate).ToString("yyyy/MM/dd"));
                                    ckfhbg_JianhuoSLs += (long)_fh.JianhuoSL;
                                    if (_fh.FuheSL != null)
                                        ckfhbg_FuheSLs += (long)_fh.FuheSL;

                                    dtfh.Rows.Add(drfh);
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["CKFuheBaoGao"]));

                            DataTable dtckfh = _rds.Tables["CKFuheBaoGao_Title"];
                            DataRow drckfh = dtckfh.NewRow();
                            wms_chukudan _ckfhds = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                            if (_ckfhds != null)
                            {
                                drckfh["ChukudanBH"] = _ckfhds.ChukudanBH;
                                drckfh["Yunsongdizhi"] = _ckfhds.Yunsongdizhi;
                                drckfh["Beizhu"] = _ckfhds.Beizhu;
                                drckfh["KehuMC"] = _ckfhds.KehuMC;
                                drckfh["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", _ckfhds.ChukuRQ == null ? "" : ((DateTime)_ckfhds.ChukuRQ).ToString("yyyy/MM/dd"));
                                drckfh["Lianxiren"] = _ckfhds.Lianxiren;
                                drckfh["LianxiDH"] = _ckfhds.LianxiDH;
                                base_weituokehu fh_wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _ckfhds.HuozhuID && p.IsDelete == false);
                                if (fh_wtkhdata != null)
                                {
                                    drckfh["HuozhuID"] = fh_wtkhdata.Kehumingcheng;
                                }
                                drckfh["ckfhbgjhSLs"] = ckfhbg_JianhuoSLs;
                                drckfh["ckfhbgfhSLs"] = ckfhbg_FuheSLs;
                                dtckfh.Rows.Add(drckfh);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["CKFuheBaoGao_Title"]));
                            break;
                        case "CKFuhejianyan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptCKFuheJYdan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtfhjy = _rds.Tables["CKFuheJilu"];
                            DataRow drfhjy;
                            long ckfujy_JianhuoSLs = 0;
                            try
                            {
                                var _fhjys = ServiceFactory.quan_chukufhservice.GetOutcheckByCK(int.Parse(_outid), p => p.JianhuoSL > 0).ToList<quan_outcheck_v>();
                                foreach (quan_outcheck_v _fh in _fhjys)
                                {
                                    drfhjy = dtfhjy.NewRow();
                                    drfhjy["ShangpinMC"] = _fh.ShangpinMC;
                                    drfhjy["Zhucezheng"] = _fh.Zhucezheng;
                                    drfhjy["Guige"] = _fh.Guige;
                                    drfhjy["Pihao"] = _fh.Pihao;
                                    drfhjy["Pihao1"] = _fh.Pihao1;
                                    drfhjy["Xuliema"] = _fh.Xuliema;
                                    drfhjy["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShengchanRQ == null ? "" : ((DateTime)_fh.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drfhjy["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _fh.ShixiaoRQ == null ? "" : ((DateTime)_fh.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drfhjy["JianhuoSL"] = _fh.JianhuoSL;
                                    drfhjy["Changjia"] = _fh.Changjia;
                                    drfhjy["Chandi"] = _fh.Chandi;
                                    ckfujy_JianhuoSLs += (long)_fh.JianhuoSL;
                                    dtfhjy.Rows.Add(drfhjy);
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["CKFuheJilu"]));

                            DataTable dtckfhjy = _rds.Tables["CKFuheJilu_Title"];
                            DataRow drckfhjy = dtckfhjy.NewRow();
                            try
                            {
                                wms_chukudan _ckfhjys = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_outid) && p.IsDelete == false);
                                if (_ckfhjys != null)
                                {
                                    drckfhjy["ChukudanBH"] = _ckfhjys.ChukudanBH;
                                    drckfhjy["Yunsongdizhi"] = _ckfhjys.Yunsongdizhi;
                                    drckfhjy["Beizhu"] = _ckfhjys.Beizhu;
                                    drckfhjy["KehuMC"] = _ckfhjys.KehuMC;
                                    drckfhjy["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", _ckfhjys.ChukuRQ == null ? "" : ((DateTime)_ckfhjys.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drckfhjy["Lianxiren"] = _ckfhjys.Lianxiren;
                                    drckfhjy["LianxiDH"] = _ckfhjys.LianxiDH;
                                }
                                base_weituokehu fhjy_wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == _ckfhjys.HuozhuID && p.IsDelete == false);
                                if (fhjy_wtkhdata != null)
                                {
                                    drckfhjy["HuozhuID"] = fhjy_wtkhdata.Kehumingcheng;
                                }
                                drckfhjy["ckfhjyjhSLs"] = ckfujy_JianhuoSLs;
                                dtckfhjy.Rows.Add(drckfhjy);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["CKFuheJilu_Title"]));
                            break;
                        case "YanShouBaoGao":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptRKyanshouBGdan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtzlysbg = _rds.Tables["RKYanshouBaoGao"];
                            DataRow drzlysbg;
                            long rkysbg_Shuliangs = 0;
                            long rkysbg_YanshouSL_Y = 0;
                            long rkysbg_YanshouSL_N = 0;
                            try
                            {
                                var _zlysbgs = ServiceFactory.quan_rukuysservice.GetEntrycheckByRK(int.Parse(_rkysid)).ToList<quan_entrycheck_v>();
                                foreach (quan_entrycheck_v _pr in _zlysbgs)
                                {
                                    drzlysbg = dtzlysbg.NewRow();
                                    //供应商
                                    wms_rukudan rkd = ServiceFactory.wms_rukudanservice.GetEntityById(s => s.ID == int.Parse(_rkysid));
                                    if (rkd != null)
                                    {
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == rkd.GongyingshangID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drzlysbg["GYSMingcheng"] = gys.Mingcheng;
                                        }
                                        drzlysbg["ystime"] = string.Format("{0:yyyy/MM/dd}", rkd.RukuRQ == null ? "" : ((DateTime)rkd.RukuRQ).ToString("yyyy/MM/dd"));
                                        drzlysbg["ChunyunYQ"] = rkd.ChunyunYQ;
                                    }
                                    drzlysbg["Changjia"] = _pr.Changjia;
                                    drzlysbg["ShangpinMC"] = _pr.ShangpinMC;
                                    drzlysbg["Guige"] = _pr.Guige;
                                    drzlysbg["Pihao"] = _pr.Pihao;
                                    drzlysbg["Xuliema"] = _pr.Xuliema;
                                    drzlysbg["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ == null ? "" : ((DateTime)_pr.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drzlysbg["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drzlysbg["Zhucezheng"] = _pr.Zhucezheng;
                                    drzlysbg["Shuliang"] = _pr.Shuliang;
                                    rkysbg_Shuliangs += (long)_pr.Shuliang;
                                    //验收结果
                                    if (_pr.ysresult == null)
                                    {
                                        drzlysbg["ysresult"] = "未验收";
                                        drzlysbg["YanshouHGSL"] = "0";
                                        drzlysbg["YanshouBHGSL"] = "0";
                                    }
                                    else
                                    {
                                        drzlysbg["ysresult"] = MvcApplication.CheckResult[(int)_pr.ysresult];
                                        var ysresult = MvcApplication.CheckResult[(int)_pr.ysresult];
                                        if (ysresult == "合格")
                                        {
                                            drzlysbg["YanshouHGSL"] = _pr.YanshouSL;
                                            drzlysbg["YanshouBHGSL"] = "0";
                                            rkysbg_YanshouSL_Y += (long)_pr.YanshouSL;
                                        }
                                        if (ysresult == "不合格")
                                        {
                                            drzlysbg["YanshouHGSL"] = "0";
                                            drzlysbg["YanshouBHGSL"] = _pr.YanshouSL;
                                            rkysbg_YanshouSL_N += (long)_pr.YanshouSL;
                                        }
                                    }
                                    dtzlysbg.Rows.Add(drzlysbg);
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RKYanshouBaoGao"]));

                            DataTable dtrkfhjy = _rds.Tables["RKYanshouBaoGao_Title"];
                            DataRow drrkfhjy = dtrkfhjy.NewRow();
                            try
                            {
                                wms_rukudan rkd_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkysid) && p.IsDelete == false);
                                if (rkd_others != null)
                                {
                                    base_weituokehu wtkh_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rkd_others.HuozhuID);
                                    drrkfhjy["HuozhuID"] = wtkh_others.Kehumingcheng;
                                }
                                var _ysbgs = ServiceFactory.quan_rukuysservice.GetEntrycheckByRK(int.Parse(_rkysid)).ToList<quan_entrycheck_v>();
                                foreach(var _date in _ysbgs)
                                {
                                    var ystime = _date.ystime;
                                    if (ystime != null)
                                        drrkfhjy["MakeDate"] = string.Format("{0:yyyy/MM/dd}", ystime == null ? "": ((DateTime)ystime).ToString("yyyy/MM/dd"));
                                    break;
                                }
                                userinfo man = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == (int)Session["user_id"] && p.IsDelete == false);
                                if (man != null)
                                {
                                    drrkfhjy["MakeMan"] = man.FullName;
                                }
                                
                                drrkfhjy["rkysbgSLs"] = rkysbg_Shuliangs;
                                drrkfhjy["rkysbgYs"] = rkysbg_YanshouSL_Y;
                                drrkfhjy["rkysbgNs"] = rkysbg_YanshouSL_N;

                                dtrkfhjy.Rows.Add(drrkfhjy);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RKYanshouBaoGao_Title"]));
                            break;
                        case "YanShouJiLu":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptRKYanShouJiLu.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtrkysjl = _rds.Tables["RKYanshouJilu"];
                            DataRow drrkysjl;
                            long ysjl_Shuliangs = 0;
                            try
                            {
                                var _rkysjls = ServiceFactory.quan_rukuysservice.GetEntrycheckByRK(int.Parse(_rkysid)).ToList<quan_entrycheck_v>();
                                foreach (quan_entrycheck_v _pr in _rkysjls)
                                {
                                    drrkysjl = dtrkysjl.NewRow();
                                    //供应商
                                    wms_rukudan rkd = ServiceFactory.wms_rukudanservice.GetEntityById(s => s.ID == int.Parse(_rkysid));
                                    if (rkd != null)
                                    {
                                        base_gongyingshang gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == rkd.GongyingshangID && p.IsDelete == false);
                                        if (gys != null)
                                        {
                                            drrkysjl["GYSMingcheng"] = gys.Mingcheng;
                                        }
                                    }
                                    drrkysjl["Changjia"] = _pr.Changjia;
                                    drrkysjl["ShangpinMC"] = _pr.ShangpinMC;
                                    drrkysjl["Guige"] = _pr.Guige;
                                    drrkysjl["Pihao"] = _pr.Pihao;
                                    drrkysjl["Xuliema"] = _pr.Xuliema;
                                    drrkysjl["ystime"] = string.Format("{0:yyyy/MM/dd}", rkd.RukuRQ == null ? "" : ((DateTime)rkd.RukuRQ).ToString("yyyy/MM/dd"));
                                    drrkysjl["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ == null ? "" : ((DateTime)_pr.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drrkysjl["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drrkysjl["Zhucezheng"] = _pr.Zhucezheng;
                                    drrkysjl["Shuliang"] = _pr.Shuliang;
                                    drrkysjl["ChunyunYQ"] = rkd.ChunyunYQ;

                                    ysjl_Shuliangs += (long)_pr.Shuliang;

                                    dtrkysjl.Rows.Add(drrkysjl);
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RKYanshouJilu"]));

                            DataTable dtysjl = _rds.Tables["RKYanshouJilu_Title"];
                            DataRow drysjl = dtysjl.NewRow();
                            try
                            {
                                wms_rukudan rkjl_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkysid) && p.IsDelete == false);
                                if (rkjl_others != null)
                                {
                                    base_weituokehu wtkhjl_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rkjl_others.HuozhuID && p.IsDelete == false);
                                    if (wtkhjl_others != null)
                                    {
                                        drysjl["HuozhuID"] = wtkhjl_others.Kehumingcheng;
                                    }
                                }
                                drysjl["rkysjlSLs"] = ysjl_Shuliangs;
                                dtysjl.Rows.Add(drysjl);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RKYanshouJilu_Title"]));
                            break;
                        case "RuKuXiangDan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptShouHuoList.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtshlist = _rds.Tables["RuKuXiangDan"];
                            DataRow drshlist;
                            long rkxd_Shuliangs = 0;
                            try
                            {
                                var _shlist = ServiceFactory.wms_cunhuoservice.GetUploadList(int.Parse(_rkxdid)).ToList<wms_upload_v>();
                                foreach (wms_upload_v _sh in _shlist)
                                {
                                    drshlist = dtshlist.NewRow();
                                    drshlist["ShangpinMC"] = _sh.ShangpinMC;
                                    drshlist["Zhucezheng"] = _sh.Zhucezheng;
                                    drshlist["Guige"] = _sh.Guige;
                                    drshlist["Pihao"] = _sh.Pihao;
                                    drshlist["Pihao1"] = _sh.Pihao1;
                                    drshlist["Xuliema"] = _sh.Xuliema;
                                    drshlist["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _sh.ShengchanRQ == null ? "" : ((DateTime)_sh.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drshlist["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _sh.ShixiaoRQ == null ? "" : ((DateTime)_sh.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drshlist["Changjia"] = _sh.Changjia;
                                    drshlist["Chandi"] = _sh.Chandi;
                                    if (_sh.sshuliang == null)
                                        _sh.sshuliang = 0;
                                    drshlist["Shuliang"] = _sh.sshuliang;
                                    drshlist["Kuwei"] = _sh.Kuwei;
                                    drshlist["CunhuoSM"] = _sh.CunhuoSM;
                                    drshlist["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _sh.MakeDate == null ? "" : ((DateTime)_sh.MakeDate).ToString("yyyy/MM/dd"));
                                    rkxd_Shuliangs += (long)_sh.sshuliang;

                                    dtshlist.Rows.Add(drshlist);
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RuKuXiangDan"]));

                            DataTable dtshlist_others = _rds.Tables["RuKuXiangDan_Title"];
                            DataRow drshlist_others = dtshlist_others.NewRow();
                            try
                            {
                                wms_rukudan rkshlist_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkxdid) && p.IsDelete == false);
                                if (rkshlist_others != null)
                                {
                                    drshlist_others["RukudanBH"] = rkshlist_others.RukudanBH;
                                    drshlist_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", rkshlist_others.RukuRQ == null ? "" : ((DateTime)rkshlist_others.RukuRQ).ToString("yyyy/MM/dd"));
                                    drshlist_others["ChunyunYQ"] = rkshlist_others.ChunyunYQ;
                                    base_weituokehu wtkhshlist_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rkshlist_others.HuozhuID && p.IsDelete == false);
                                    if (wtkhshlist_others != null)
                                    {
                                        drshlist_others["HuozhuID"] = wtkhshlist_others.Kehumingcheng;
                                    }
                                }
                                drshlist_others["rkxdSLs"] = rkxd_Shuliangs;

                                dtshlist_others.Rows.Add(drshlist_others);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RuKuXiangDan_Title"]));
                            break;
                        case "ShangJiaList":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptShangJiaList.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtsjlist = _rds.Tables["ShangJiaList"];
                            DataRow drsjlist;
                            long sjl_sshuliangs = 0;
                            try
                            {
                                var _sjlist = ServiceFactory.wms_cunhuoservice.GetUploadList(int.Parse(_sjdid)).ToList<wms_upload_v>();
                                foreach (wms_upload_v _sh in _sjlist)
                                {
                                    drsjlist = dtsjlist.NewRow();
                                    drsjlist["ShangpinMC"] = _sh.ShangpinMC;
                                    drsjlist["Guige"] = _sh.Guige;
                                    drsjlist["Pihao"] = _sh.Pihao;
                                    drsjlist["Pihao1"] = _sh.Pihao1;
                                    drsjlist["Xuliema"] = _sh.Xuliema;
                                    drsjlist["Shuliang"] = _sh.sshuliang == null ? float.Parse("0") : _sh.sshuliang;
                                    drsjlist["Kuwei"] = _sh.Kuwei;
                                    drsjlist["CunhuoSM"] = _sh.CunhuoSM;
                                    drsjlist["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _sh.MakeDate == null ? "" : ((DateTime)_sh.MakeDate).ToString("yyyy/MM/dd"));
                                    if(_sh.sshuliang!=null)
                                        sjl_sshuliangs += (long)_sh.sshuliang;

                                    dtsjlist.Rows.Add(drsjlist);
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["ShangJiaList"]));

                            DataTable dtsjlist_others = _rds.Tables["ShangJiaList_Title"];
                            DataRow drsjlist_others = dtsjlist_others.NewRow();
                            try
                            {
                                wms_rukudan rksjlist_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_sjdid) && p.IsDelete == false);
                                if (rksjlist_others != null)
                                {
                                    base_weituokehu wtkhsjlist_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == rksjlist_others.HuozhuID);
                                    if (wtkhsjlist_others != null)
                                    {
                                        drsjlist_others["HuozhuID"] = wtkhsjlist_others.Kehumingcheng;
                                    }
                                    drsjlist_others["RukudanBH"] = rksjlist_others.RukudanBH;
                                    drsjlist_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", rksjlist_others.RukuRQ==null?"": ((DateTime)rksjlist_others.RukuRQ).ToString("yyyy/MM/dd"));
                                    drsjlist_others["ChunyunYQ"] = rksjlist_others.ChunyunYQ;
                                }
                                drsjlist_others["sjlSLs"] = sjl_sshuliangs;

                                dtsjlist_others.Rows.Add(drsjlist_others);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["ShangJiaList_Title"]));
                            break;
                        case "RuKuMX":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptRuKumingxi.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtRuKuMX = _rds.Tables["RuKumingxi"];
                            DataRow drRuKuMX;
                            long rkmx_DaohuoSLs = 0;
                            try
                            {
                                //_RuKuMXs:'规格'&'批号'&'注册证'&'失效日期'&'到货数量'.
                                var _RuKuMXs = ServiceFactory.wms_rukumxservice.LoadSortEntities(p => p.RukuID == int.Parse(_rkmxid) && p.IsDelete == false, true, s => s.ShangpinMC);
                                foreach (wms_rukumx _pr in _RuKuMXs)
                                {
                                    drRuKuMX = dtRuKuMX.NewRow();
                                    //drRuKuMX["ShangpinMC"] = _pr.ShangpinMC;
                                    drRuKuMX["Guige"] = _pr.Guige;
                                    drRuKuMX["Pihao"] = _pr.Pihao;
                                    drRuKuMX["Zhucezheng"] = _pr.Zhucezheng;
                                    //drRuKuMX["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ);
                                    drRuKuMX["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drRuKuMX["DaohuoSL"] = _pr.DaohuoSL;
                                    //drRuKuMX["Chandi"] = _pr.Chandi;
                                    rkmx_DaohuoSLs += (long)_pr.DaohuoSL;

                                    dtRuKuMX.Rows.Add(drRuKuMX);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["RuKumingxi"]));

                            DataTable dtRuKuMX_others = _rds.Tables["RuKumingxi_Title"];
                            DataRow drRuKuMX_others = dtRuKuMX_others.NewRow();
                            try
                            {
                                //wms_rukudan:'入库单编号'&'入库日期'&'储运要求'.
                                wms_rukudan RuKuMX_others = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.ID == int.Parse(_rkmxid));
                                if (RuKuMX_others != null)
                                {
                                    drRuKuMX_others["RukudanBH"] = RuKuMX_others.RukudanBH;
                                    drRuKuMX_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", RuKuMX_others.RukuRQ == null ? "" : ((DateTime)RuKuMX_others.RukuRQ).ToString("yyyy/MM/dd"));
                                    drRuKuMX_others["ChunyunYQ"] = RuKuMX_others.ChunyunYQ;
                                    //base_weituokehu:'货主'.
                                    base_weituokehu wtkhRuKuMX_others = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == RuKuMX_others.HuozhuID);
                                    if (wtkhRuKuMX_others != null)
                                    {
                                        drRuKuMX_others["HuozhuID"] = wtkhRuKuMX_others.Kehumingcheng;
                                    }
                                    //base_gongyingshang:'供应商'.
                                    base_gongyingshang RuKuMXgys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == RuKuMX_others.GongyingshangID);
                                    if (RuKuMXgys != null)
                                    {
                                        drRuKuMX_others["GongyingshangID"] = RuKuMXgys.Mingcheng;
                                    }
                                    //wms_cangku:'仓库名'
                                    wms_cangku RuKuMX_ck = ServiceFactory.wms_cangkuservice.GetEntityById(p => p.ID == RuKuMX_others.CangkuID);
                                    if (RuKuMX_ck != null)
                                    {
                                        drRuKuMX_others["CangkuID"] = RuKuMX_ck.Mingcheng;
                                    }
                                }
                                //到货数量总计
                                drRuKuMX_others["rkmxdhSLs"] = rkmx_DaohuoSLs;

                                dtRuKuMX_others.Rows.Add(drRuKuMX_others);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["RuKumingxi_Title"]));
                            break;
                        case "zlgl_RukuYanshouBaogao":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptZlgl_RKBG.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtzlgl_bg = _rds.Tables["zlgl_RKYSBG"];
                            DataRow drzlgl_bg;
                            long zlgl_bg_Shuliang = 0;
                            long zlgl_bg_YanshouHGSL = 0;
                            long zlgl_bg_YanshouBHGSL = 0;
                            try
                            {
                                var _zlgl_rkyss = ServiceFactory.quan_rukuysservice.GetInrec(p => p.RukudanBH == _zlgl_rkysid && p.IsDelete == false).ToList<quan_inrec_v>();
                                foreach (quan_inrec_v _pr in _zlgl_rkyss)
                                {
                                    drzlgl_bg = dtzlgl_bg.NewRow();
                                    base_gongyingshang zlgl_gys = ServiceFactory.base_gongyingshangservice.GetEntityById(p => p.ID == _pr.GongyingshangID && p.IsDelete == false);
                                    if (zlgl_gys != null)
                                    {
                                        drzlgl_bg["GongyingshangID"] = zlgl_gys.Mingcheng;
                                    }
                                    drzlgl_bg["Changjia"] = _pr.Changjia;
                                    drzlgl_bg["ShangpinMC"] = _pr.ShangpinMC;
                                    drzlgl_bg["Guige"] = _pr.Guige;
                                    drzlgl_bg["Pihao"] = _pr.Pihao;
                                    drzlgl_bg["Xuliema"] = _pr.Xuliema;
                                    drzlgl_bg["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ);
                                    drzlgl_bg["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ);
                                    drzlgl_bg["Zhucezheng"] = _pr.Zhucezheng;
                                    drzlgl_bg["Shuliang"] = _pr.Shuliang;
                                    //验收结果
                                    drzlgl_bg["ysresult"] = MvcApplication.CheckResult[_pr.ysresult];
                                    var ysresult = MvcApplication.CheckResult[_pr.ysresult];
                                    if (ysresult == "合格")
                                    {
                                        drzlgl_bg["YanshouHGSL"] = _pr.YanshouSL;
                                        drzlgl_bg["YanshouBHGSL"] = "0";
                                        zlgl_bg_YanshouHGSL += (long)_pr.YanshouSL;
                                    }
                                    if (ysresult == "不合格")
                                    {
                                        drzlgl_bg["YanshouHGSL"] = "0";
                                        drzlgl_bg["YanshouBHGSL"] = _pr.YanshouSL;
                                        zlgl_bg_YanshouBHGSL += (long)_pr.YanshouSL;
                                    }
                                    drzlgl_bg["ystime"] = string.Format("{0:yyyy/MM/dd}", _pr.ystime == null ? "" : _pr.ystime.ToString("yyyy/MM/dd"));
                                    zlgl_bg_Shuliang += (long)_pr.Shuliang;

                                    dtzlgl_bg.Rows.Add(drzlgl_bg);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["zlgl_RKYSBG"]));

                            DataTable dtzlgl_bg_others = _rds.Tables["zlgl_RKYSBG_Title"];
                            DataRow drzlgl_bg_others = dtzlgl_bg_others.NewRow();
                            try
                            {
                                var _zlgl_rkyss = ServiceFactory.quan_rukuysservice.GetInrec(p => p.RukudanBH == _zlgl_rkysid && p.IsDelete == false).ToList<quan_inrec_v>();
                                //得到第一个货主ID
                                quan_inrec_v zlgl_huozhiid_first = _zlgl_rkyss.FirstOrDefault();
                                if (zlgl_huozhiid_first != null)
                                {
                                    base_weituokehu zlgl_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == zlgl_huozhiid_first.HuozhuID);
                                    if (zlgl_wtkh != null)
                                    {
                                        drzlgl_bg_others["HuozhuID"] = zlgl_wtkh.Kehumingcheng;
                                    }
                                }
                                userinfo zlgl_man = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == (int)Session["user_id"]);
                                if (zlgl_man != null)
                                {
                                    drzlgl_bg_others["MakeMan"] = zlgl_man.FullName;
                                }
                                wms_rukudan zlgl_rkd = ServiceFactory.wms_rukudanservice.GetEntityById(p => p.RukudanBH == _zlgl_rkysid);
                                if (zlgl_rkd != null)
                                {
                                    drzlgl_bg_others["RukuRQ"] = string.Format("{0:yyyy/MM/dd}", zlgl_rkd.RukuRQ);

                                }
                                drzlgl_bg_others["rkysbgSLs"] = zlgl_bg_Shuliang;
                                drzlgl_bg_others["rkysbgYs"] = zlgl_bg_YanshouHGSL;
                                drzlgl_bg_others["rkysbgNs"] = zlgl_bg_YanshouBHGSL;

                                dtzlgl_bg_others.Rows.Add(drzlgl_bg_others);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["zlgl_RKYSBG_Title"]));
                            break;
                        case "zlgl_CukuFuheBaogao":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptZlgl_CKBG.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtzlgl_ckfhbg = _rds.Tables["zlgl_CKFHBG"];
                            DataRow drzlgl_ckfhbg;
                            long zlgl_ckfhbg_Shuliang = 0;
                            long zlgl_ckfhbg_FuheSL = 0;
                            long zlgl_ckfhbg_YanshouHGSL = 0;
                            long zlgl_ckfhbg_YanshouBHGSL = 0;
                            try
                            {
                                var _zlgl_ckfhs = ServiceFactory.quan_chukufhservice.GetOutrec(p => p.ChukudanBH == _zlgl_ckfhid).ToList<quan_outrec_v>();
                                foreach (quan_outrec_v _pr in _zlgl_ckfhs)
                                {
                                    drzlgl_ckfhbg = dtzlgl_ckfhbg.NewRow();
                                    drzlgl_ckfhbg["ShangpinMC"] = _pr.ShangpinMC;
                                    drzlgl_ckfhbg["Zhucezheng"] = _pr.Zhucezheng;
                                    drzlgl_ckfhbg["Guige"] = _pr.Guige;
                                    drzlgl_ckfhbg["Pihao"] = _pr.Pihao;
                                    drzlgl_ckfhbg["Pihao1"] = _pr.Pihao1;
                                    drzlgl_ckfhbg["Xuliema"] = _pr.Xuliema;
                                    drzlgl_ckfhbg["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShengchanRQ == null ? "" : ((DateTime)_pr.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drzlgl_ckfhbg["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pr.ShixiaoRQ == null ? "" : ((DateTime)_pr.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drzlgl_ckfhbg["JianhuoSL"] = _pr.JianhuoSL;
                                    drzlgl_ckfhbg["Changjia"] = _pr.Changjia;
                                    drzlgl_ckfhbg["Chandi"] = _pr.Chandi;
                                    //验收结果
                                    drzlgl_ckfhbg["Fuhe"] = MvcApplication.CheckResult[_pr.Fuhe];
                                    drzlgl_ckfhbg["FuheSL"] = _pr.FuheSL;
                                    //var FuheResult = MvcApplication.CheckResult[_pr.Fuhe];
                                    //if (FuheResult == "合格")
                                    //{
                                    //    drzlgl_ckfhbg["FuheHGSL"] = _pr.FuheSL;
                                    //    drzlgl_ckfhbg["FuheBHGSL"] = "";
                                    //    zlgl_ckfhbg_YanshouHGSL += (long)_pr.FuheSL;
                                    //}
                                    //if (FuheResult == "不合格")
                                    //{
                                    //    drzlgl_ckfhbg["FuheHGSL"] = "";
                                    //    drzlgl_ckfhbg["FuheBHGSL"] = _pr.FuheSL;
                                    //    zlgl_ckfhbg_YanshouBHGSL += (long)_pr.FuheSL;
                                    //}
                                    drzlgl_ckfhbg["Fuheren"] = _pr.Fuheren;
                                    drzlgl_ckfhbg["FuheSM"] = _pr.FuheSM;
                                    drzlgl_ckfhbg["MakeDate"] = string.Format("{0:yyyy/MM/dd}", _pr.MakeDate == null ? "" : ((DateTime)_pr.MakeDate).ToString("yyyy/MM/dd"));
                                    zlgl_ckfhbg_Shuliang += (long)_pr.JianhuoSL;
                                    zlgl_ckfhbg_FuheSL += (long)_pr.FuheSL;

                                    dtzlgl_ckfhbg.Rows.Add(drzlgl_ckfhbg);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["zlgl_CKFHBG"]));

                            DataTable dtzlgl_ckfhbg_others = _rds.Tables["zlgl_CKFHBG_Title"];
                            DataRow drzlgl_ckfhbg_others = dtzlgl_ckfhbg_others.NewRow();
                            try
                            {
                                drzlgl_ckfhbg_others["ChukudanBH"] = _zlgl_ckfhid;
                                //得到第一个ID
                                var _zlgl_ckfhs = ServiceFactory.quan_chukufhservice.GetOutrec(p => p.ChukudanBH == _zlgl_ckfhid).ToList<quan_outrec_v>();
                                if (_zlgl_ckfhs != null)
                                {
                                    quan_outrec_v zlgl_first = _zlgl_ckfhs.FirstOrDefault();
                                    if (zlgl_first != null)
                                    {
                                        drzlgl_ckfhbg_others["KehuMC"] = zlgl_first.KehuMC;
                                        drzlgl_ckfhbg_others["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", zlgl_first.ChukuRQ == null ? "" : ((DateTime)zlgl_first.ChukuRQ).ToString("yyyy/MM/dd"));
                                        base_weituokehu zlgl_ckfh_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == zlgl_first.HuozhuID);
                                        if (zlgl_ckfh_wtkh != null)
                                        {
                                            drzlgl_ckfhbg_others["HuozhuID"] = zlgl_ckfh_wtkh.Kehumingcheng;
                                        }
                                    }
                                }
                                wms_chukudan zlgl_ckd = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ChukudanBH == _zlgl_ckfhid && p.IsDelete == false);
                                if (zlgl_ckd != null)
                                {
                                    drzlgl_ckfhbg_others["Yunsongdizhi"] = zlgl_ckd.Yunsongdizhi;
                                    drzlgl_ckfhbg_others["Beizhu"] = zlgl_ckd.Beizhu;
                                    drzlgl_ckfhbg_others["Lianxiren"] = zlgl_ckd.Lianxiren;
                                    drzlgl_ckfhbg_others["LianxiDH"] = zlgl_ckd.LianxiDH;
                                }
                                //统计
                                drzlgl_ckfhbg_others["ckfhbgSLs"] = zlgl_ckfhbg_Shuliang;
                                drzlgl_ckfhbg_others["ckfhbgYs"] = zlgl_ckfhbg_YanshouHGSL;
                                drzlgl_ckfhbg_others["ckfhbgNs"] = zlgl_ckfhbg_YanshouBHGSL;
                                drzlgl_ckfhbg_others["ckfhbgFHSLs"] = zlgl_ckfhbg_FuheSL;

                                dtzlgl_ckfhbg_others.Rows.Add(drzlgl_ckfhbg_others);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["zlgl_CKFHBG_Title"]));
                            break;
                        case "shangpinxx_shouying":
                            int userid = (int)Session["user_id"];
                            string pagetag = "base_shangpinxx_index";
                            Expression<Func<base_shangpin_v, bool>> where = PredicateExtensionses.True<base_shangpin_v>();
                            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
                            if (sc != null)
                            {
                                string[] sclist = sc.ConditionInfo.Split(';');
                                foreach (string scl in sclist)
                                {
                                    string[] scld = scl.Split(',');
                                    switch (scld[0])
                                    {
                                        case "huozhuid":
                                            string huozhuid = scld[1];
                                            string huozhuidequal = scld[2];
                                            string huozhuidand = scld[3];
                                            if (!string.IsNullOrEmpty(huozhuid))
                                            {
                                                if (huozhuidequal.Equals("="))
                                                {
                                                    if (huozhuidand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                                                }
                                            }
                                            break;
                                        case "daima":
                                            string daima = scld[1];
                                            string daimaequal = scld[2];
                                            string daimaand = scld[3];
                                            if (!string.IsNullOrEmpty(daima))
                                            {
                                                if (daimaequal.Equals("="))
                                                {
                                                    if (daimaand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.daima == daima);
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.daima == daima);
                                                }
                                                if (daimaequal.Equals("like"))
                                                {
                                                    if (daimaand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                                                }
                                            }
                                            break;
                                        case "mingcheng":
                                            string mingcheng = scld[1];
                                            string mingchengequal = scld[2];
                                            string mingchengand = scld[3];
                                            if (!string.IsNullOrEmpty(mingcheng))
                                            {
                                                if (mingchengequal.Equals("="))
                                                {
                                                    if (mingchengand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                                                }
                                                if (mingchengequal.Equals("like"))
                                                {
                                                    if (mingchengand.Equals("and"))
                                                        where = where.And(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                                                    else
                                                        where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                                                }
                                            }
                                            break;
                                        case "zhucezhengbh":
                                            string zhucezhengbh = scld[1];
                                            string zhucezhengbhequal = scld[2];
                                            string zhucezhengbhand = scld[3];
                                            if (!string.IsNullOrEmpty(zhucezhengbh))
                                            {
                                                if (zhucezhengbhequal.Equals("="))
                                                {
                                                    if (zhucezhengbhand.Equals("and"))
                                                        where = where.And(p => p.ZhucezhengBH == zhucezhengbh);
                                                    else
                                                        where = where.Or(p => p.ZhucezhengBH == zhucezhengbh);
                                                }
                                                if (zhucezhengbhequal.Equals("like"))
                                                {
                                                    if (zhucezhengbhand.Equals("and"))
                                                        where = where.And(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                                                    else
                                                        where = where.Or(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                                                }
                                            }
                                            break;
                                        case "shouying":
                                            string shouying = scld[1];
                                            string shouyingequal = scld[2];
                                            string shouyingand = scld[3];
                                            if (shouying == "0")
                                            {
                                                shouying = "";
                                            }
                                            if (!string.IsNullOrEmpty(shouying))
                                            {
                                                if (shouyingequal.Equals("="))
                                                {
                                                    if (shouyingand.Equals("and"))
                                                        where = where.And(p => p.Shouying == int.Parse(shouying));
                                                    else
                                                        where = where.Or(p => p.Shouying == int.Parse(shouying));
                                                }
                                            }
                                            break;
                                        case "shenchasf":
                                            string shenchasf = scld[1];
                                            string shenchasfequal = scld[2];
                                            string shenchasfand = scld[3];
                                            if (shenchasf == "1")
                                            {
                                                shenchasf = "true";
                                            }
                                            else if (shenchasf == "2")
                                            {
                                                shenchasf = "false";
                                            }
                                            else if (shenchasf == "-1")
                                            {
                                                shenchasf = "";
                                            }
                                            if (!string.IsNullOrEmpty(shenchasf))
                                            {
                                                if (shenchasfequal.Equals("="))
                                                {
                                                    if (shenchasfand.Equals("and"))
                                                        where = where.And(p => p.ShenchaSF == bool.Parse(shenchasf));
                                                    else
                                                        where = where.Or(p => p.ShenchaSF == bool.Parse(shenchasf));
                                                }
                                            }
                                            break;
                                        case "gongyingid":
                                            string gongyingid = scld[1];
                                            string gongyingidequal = scld[2];
                                            string gongyingidand = scld[3];
                                            if (!string.IsNullOrEmpty(gongyingid))
                                            {
                                                if (gongyingidequal.Equals("="))
                                                {
                                                    if (gongyingidand.Equals("and"))
                                                        where = where.And(p => p.GongyingID == int.Parse(gongyingid));
                                                    else
                                                        where = where.Or(p => p.GongyingID == int.Parse(gongyingid));
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            where = where.And(base_shangpin_v => base_shangpin_v.IsDelete == false);

                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptSpxx_shouying.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtspxx = _rds.Tables["Spxx_shouying"];
                            DataRow drspxx;
                            try
                            {
                                var _spxxs = ServiceFactory.base_shangpinxxservice.LoadShangpinAll(where.Compile()).ToList<base_shangpin_v>();
                                foreach (base_shangpin_v _pr in _spxxs)
                                {
                                    drspxx = dtspxx.NewRow();
                                    //base_shangpinxx
                                    drspxx["ZhucezhengBH"] = _pr.ZhucezhengBH;
                                    drspxx["Daima"] = _pr.daima;
                                    drspxx["Guige"] = _pr.Guige;
                                    drspxx["Chandi"] = _pr.chandi;
                                    drspxx["Mingcheng"] = _pr.mingcheng;
                                    //base_shangpinzcz
                                    var _spzczs = ServiceFactory.base_shangpinzczservice.GetEntityById(p => p.ID == _pr.zhucezhengid && p.IsDelete == false);
                                    if (_spzczs != null)
                                    {
                                        drspxx["ZhucezhengYXQ"] = string.Format("{0:yyyy/MM/dd}", _spzczs.ZhucezhengYXQ == null ? "" : ((DateTime)_spzczs.ZhucezhengYXQ).ToString("yyyy/MM/dd"));
                                    }
                                    //quan_shouyingsq
                                    var _sysqs = ServiceFactory.quan_shouyingsqservice.GetEntityById(p => p.ShenqingID == _pr.id && p.IsDelete == false && p.Zhuangtai == 5);
                                    if (_sysqs != null)
                                    {
                                        drspxx["Shenheshuoming"] = _sysqs.Shenheshuoming;
                                        drspxx["Shenheren"] = _sysqs.Shenheren;
                                        drspxx["FuzerenSM"] = _sysqs.FuzerenSM;
                                        drspxx["Fuzeren"] = _sysqs.Fuzeren;
                                        drspxx["SQshijian"] = string.Format("{0:yyyy/MM/dd}", _sysqs.SQshijian == null ? "" : ((DateTime)_sysqs.SQshijian).ToString("yyyy/MM/dd"));
                                    }
                                    //base_huozhushouquan
                                    var _hzsqs = ServiceFactory.base_huozhushouquanservice.GetEntityById(p => p.HuozhuID == _pr.huozhuid && p.ShouquanID == _pr.GongyingID && p.IsDelete == false);
                                    if (_hzsqs != null)
                                    {
                                        drspxx["ShouquanshuYXQ"] = string.Format("{0:yyyy/MM/dd}", _hzsqs.ShouquanshuYXQ == null ? "" : ((DateTime)_hzsqs.ShouquanshuYXQ).ToString("yyyy/MM/dd"));
                                    }

                                    dtspxx.Rows.Add(drspxx);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["Spxx_shouying"]));
                            //页眉&页脚
                            DataTable dtspxx_others = _rds.Tables["Spxx_shouying_Title"];
                            DataRow drspxx_others = dtspxx_others.NewRow();
                            try
                            {
                                int? first_huozhuid = -1;
                                var _spxxs = ServiceFactory.base_shangpinxxservice.LoadShangpinAll(where.Compile()).ToList<base_shangpin_v>();
                                foreach (base_shangpin_v _pr in _spxxs)
                                {
                                    if (_pr.huozhuid != null)
                                    {
                                        first_huozhuid = _pr.huozhuid;
                                        break;
                                    }
                                }
                                //base_gongyingshang
                                if (first_huozhuid >= 0)
                                {
                                    var spxx_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == first_huozhuid && p.IsDelete == false);
                                    if (spxx_wtkh != null)
                                    {
                                        drspxx_others["Kehumingcheng"] = spxx_wtkh.Kehumingcheng;
                                        drspxx_others["YingyezhizhaoBH"] = spxx_wtkh.YingyezhizhaoBH;
                                        drspxx_others["YingyezhizhaoYXQ"] = string.Format("{0:yyyy/MM/dd}", spxx_wtkh.YingyezhizhaoYXQ == null ? "" : ((DateTime)spxx_wtkh.YingyezhizhaoYXQ).ToString("yyyy/MM/dd"));
                                        drspxx_others["JingyingxukeBH"] = spxx_wtkh.JingyingxukeBH;
                                        drspxx_others["JingyingxukeYXQ"] = string.Format("{0:yyyy/MM/dd}", spxx_wtkh.JingyingxukeYXQ == null ? "" : ((DateTime)spxx_wtkh.JingyingxukeYXQ).ToString("yyyy/MM/dd"));
                                        drspxx_others["Lianxiren"] = spxx_wtkh.Lianxiren;
                                        drspxx_others["Lianxidianhua"] = spxx_wtkh.Lianxidianhua;
                                        drspxx_others["BeianBH"] = spxx_wtkh.BeianBH;
                                        drspxx_others["BeianYXQ"] = string.Format("{0:yyyy/MM/dd}", spxx_wtkh.BeianYXQ == null ? "" : ((DateTime)spxx_wtkh.BeianYXQ).ToString("yyyy/MM/dd"));
                                        dtspxx_others.Rows.Add(drspxx_others);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["Spxx_shouying_Title"]));
                            break;
                        case "YiweiInfo":
                            int yw_userid = (int)Session["user_id"];
                            string yw_pagetag = "wms_yiwei_index";
                            Expression<Func<wms_yiwei, bool>> yw_where = PredicateExtensionses.True<wms_yiwei>();
                            searchcondition yw_sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == yw_userid && searchcondition.PageBrief == yw_pagetag);
                            if (yw_sc != null && yw_sc.ConditionInfo != null)
                            {
                                string[] sclist = yw_sc.ConditionInfo.Split(';');
                                foreach (string scl in sclist)
                                {
                                    string[] scld = scl.Split(',');
                                    switch (scld[0])
                                    {
                                        case "kwbh":
                                            string kwbh = scld[1];
                                            string kwbhequal = scld[2];
                                            string kwbhand = scld[3];
                                            if (!string.IsNullOrEmpty(kwbh))
                                            {
                                                if (kwbhequal.Equals("="))
                                                {
                                                    if (kwbhand.Equals("and"))
                                                        yw_where = yw_where.And(p => p.KWBH == kwbh);
                                                    else
                                                        yw_where = yw_where.Or(p => p.KWBH == kwbh);
                                                }
                                                if (kwbhequal.Equals("like"))
                                                {
                                                    if (kwbhand.Equals("and"))
                                                        yw_where = yw_where.And(p => p.KWBH.Contains(kwbh));
                                                    else
                                                        yw_where = yw_where.Or(p => p.KWBH.Contains(kwbh));
                                                }
                                            }
                                            break;
                                        case "xkwbh":
                                            string xkwbh = scld[1];
                                            string xkwbhequal = scld[2];
                                            string xkwbhand = scld[3];
                                            if (!string.IsNullOrEmpty(xkwbh))
                                            {
                                                if (xkwbhequal.Equals("="))
                                                {
                                                    if (xkwbhand.Equals("and"))
                                                        yw_where = yw_where.And(p => p.XKWBH == xkwbh);
                                                    else
                                                        yw_where = yw_where.Or(p => p.XKWBH == xkwbh);
                                                }
                                                if (xkwbhequal.Equals("like"))
                                                {
                                                    if (xkwbhand.Equals("and"))
                                                        yw_where = yw_where.And(p => p.XKWBH.Contains(xkwbh));
                                                    else
                                                        yw_where = yw_where.Or(p => p.XKWBH.Contains(xkwbh));
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            yw_where = yw_where.And(wms_yiwei => wms_yiwei.IsDelete == false);

                            var rptYiweiInfos = ServiceFactory.wms_yiweiservice.LoadSortEntities(yw_where.Compile(), false, wms_yiwei => wms_yiwei.ID);
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptYiweiInfo.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtYiweiInfo = _rds.Tables["YiweiInfo"];
                            DataRow drYiweiInfo;
                            foreach (wms_yiwei _pr in rptYiweiInfos)
                            {
                                drYiweiInfo = dtYiweiInfo.NewRow();
                                wms_shouhuomx YiweiInfo_shmx = ServiceFactory.wms_shouhuomxservice.GetEntityById(p => p.ID == _pr.SHID);
                                drYiweiInfo["SHID"] = YiweiInfo_shmx.ShangpinMC + "," + YiweiInfo_shmx.Pihao + "," + YiweiInfo_shmx.Xuliema + "," + YiweiInfo_shmx.Guige;
                                //drYiweiInfo["SHID"] = HtmlHelperExtensions.GetDataValue_ID("收货", "全部", (int)_pr.SHID);
                                drYiweiInfo["KWBH"] = _pr.KWBH;
                                drYiweiInfo["Shuliang"] = _pr.Shuliang;
                                drYiweiInfo["XKWBH"] = _pr.XKWBH;
                                drYiweiInfo["MakeDate"] = string.Format("{0:d}", _pr.MakeDate);
                                userinfo YiweiInfo_man = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == _pr.MakeMan);
                                drYiweiInfo["MakeMan"] = YiweiInfo_man.FullName;
                                dtYiweiInfo.Rows.Add(drYiweiInfo);
                            }
                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["YiweiInfo"]));
                            break;
                        case "jianhuo_CKFuhejianyan":
                            rptView.Reset();
                            rptView.LocalReport.ReportPath = "reports/rptJH_CKFuhejianyan.rdlc";
                            rptView.LocalReport.DataSources.Clear();
                            DataTable dtjh_ckfhjy = _rds.Tables["JH_CKfuhejianyan"];
                            DataRow drjh_ckfhjy;
                            long JH_CKFHJY_SLs = 0;
                            try
                            {
                                var _jhds = ob_wms_jianhuoservice.GetPickDetail(int.Parse(_JH_ckjyid), p => p.DaijianSL > 0).OrderBy(p => p.Kuwei).ThenBy(p => p.Pihao).ToList<wms_pick_v>();
                                foreach (wms_pick_v _pv in _jhds)
                                {
                                    drjh_ckfhjy = dtjh_ckfhjy.NewRow();
                                    drjh_ckfhjy["Mingcheng"] = _pv.ShangpinMC;
                                    drjh_ckfhjy["Zhucezheng"] = _pv.Zhucezheng;
                                    drjh_ckfhjy["Guige"] = _pv.Guige;
                                    drjh_ckfhjy["Pihao"] = _pv.Pihao;
                                    drjh_ckfhjy["Pihao1"] = _pv.Pihao1;
                                    drjh_ckfhjy["Xuliema"] = _pv.Xuliema;
                                    drjh_ckfhjy["ShengchanRQ"] = string.Format("{0:yyyy/MM/dd}", _pv.ShengchanRQ == null ? "" : ((DateTime)_pv.ShengchanRQ).ToString("yyyy/MM/dd"));
                                    drjh_ckfhjy["ShixiaoRQ"] = string.Format("{0:yyyy/MM/dd}", _pv.ShixiaoRQ == null ? "" : ((DateTime)_pv.ShixiaoRQ).ToString("yyyy/MM/dd"));
                                    drjh_ckfhjy["DaijianSL"] = _pv.DaijianSL;
                                    JH_CKFHJY_SLs += (long)_pv.DaijianSL;
                                    drjh_ckfhjy["Changjia"] = _pv.Changjia;
                                    drjh_ckfhjy["Chandi"] = _pv.Chandi;

                                    dtjh_ckfhjy.Rows.Add(drjh_ckfhjy);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", _rds.Tables["JH_CKfuhejianyan"]));

                            DataTable dtjh_ckfhjy_t = _rds.Tables["JH_CKfuhejianyan_Title"];
                            DataRow drjhdt_t = dtjh_ckfhjy_t.NewRow();
                            try
                            {
                                wms_chukudan tempdata = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_JH_ckjyid) && p.IsDelete == false);
                                if (tempdata != null)
                                {
                                    drjhdt_t["Yunsongdizhi"] = tempdata.Yunsongdizhi;
                                    drjhdt_t["ChukuRQ"] = string.Format("{0:yyyy/MM/dd}", tempdata.ChukuRQ == null ? "" : ((DateTime)tempdata.ChukuRQ).ToString("yyyy/MM/dd"));
                                    drjhdt_t["ChukudanBH"] = tempdata.ChukudanBH;
                                    drjhdt_t["Lianxiren"] = tempdata.Lianxiren;
                                    drjhdt_t["LianxiDH"] = tempdata.LianxiDH;
                                    drjhdt_t["Beizhu"] = tempdata.Beizhu;
                                    drjhdt_t["KehuMC"] = tempdata.KehuMC;
                                    drjhdt_t["JH_CKFHJY_SLs"] = JH_CKFHJY_SLs;
                                    base_weituokehu wtkhdata = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == tempdata.HuozhuID && p.IsDelete == false);
                                    if (wtkhdata != null)
                                    {
                                        drjhdt_t["HuozhuID"] = wtkhdata.Kehumingcheng;
                                    }
                                    dtjh_ckfhjy_t.Rows.Add(drjhdt_t);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            rptView.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", _rds.Tables["JH_CKfuhejianyan_Title"]));
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
}