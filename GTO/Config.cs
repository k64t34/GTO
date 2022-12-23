using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;
namespace GTO
{
	
	public class Cfg
	{
		const string __ERR1_fail_write_registry = "Не удалось записать в реестр требуемые данные";
        const String regKey = @"SOFTWARE\GTO\";
        const String regHKCU_project = @"SOFTWARE\GTO\";
        public const String reg_FormTop = "FormTop";
        public const String reg_FormLeft = "FormLeft";
        public const String reg_FormWidth = "FormWidth";
        public const String reg_FormHeight = "FormHeight";
        public const String reg_FormState = "FormState";//0-normal, 1-min, 2-max

        static public void saveConfig(Form frm)
        {
            

                RegistryKey regHKCU, reg;
                try
                {
                regHKCU = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                reg = regHKCU.CreateSubKey(regHKCU_project, true);
                if (frm.WindowState == System.Windows.Forms.FormWindowState.Normal)
                {
                    
                    reg.SetValue(reg_FormTop, frm.Top, RegistryValueKind.DWord);
                    if (reg.GetValue(reg_FormTop) == null) { reg.DeleteValue(reg_FormTop); throw new Exception(__ERR1_fail_write_registry + reg.Name); }
                    reg.SetValue(reg_FormLeft, frm.Left, RegistryValueKind.DWord);
                    if (reg.GetValue(reg_FormLeft) == null) throw new Exception(__ERR1_fail_write_registry + reg.Name);
                    reg.SetValue(reg_FormWidth, frm.Width, RegistryValueKind.DWord);
                    if (reg.GetValue(reg_FormWidth) == null) throw new Exception(__ERR1_fail_write_registry + reg.Name);
                    reg.SetValue(reg_FormHeight, frm.Height, RegistryValueKind.DWord);
                    if (reg.GetValue(reg_FormHeight) == null) throw new Exception(__ERR1_fail_write_registry + reg.Name);
                    reg.SetValue(reg_FormState, 0, RegistryValueKind.DWord);
                }
                else if (frm.WindowState == System.Windows.Forms.FormWindowState.Maximized) reg.SetValue(reg_FormState, 2, RegistryValueKind.DWord);
                }
                catch (Exception e) { }           
            
            return;
        }
         static public void loadConfig(Form frm)
        {            
            RegistryKey regHKCU, reg;
            try
            {
                regHKCU = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                reg = regHKCU.CreateSubKey(regHKCU_project,false);

                if (reg.GetValue(reg_FormState) == null) throw new Exception(__ERR1_fail_write_registry + reg.Name);
                int fState = System.Convert.ToInt32(reg.GetValue(reg_FormState));

                if (fState == 2) frm.WindowState = FormWindowState.Maximized;
                else
                {
                    if (reg.GetValue(reg_FormTop) == null) throw new Exception(__ERR1_fail_write_registry + reg.Name);
                    int fTop = System.Convert.ToInt32(reg.GetValue(reg_FormTop));
                    if (reg.GetValue(reg_FormLeft) == null) throw new Exception(__ERR1_fail_write_registry + reg.Name);
                    int fLeft = System.Convert.ToInt32(reg.GetValue(reg_FormLeft));
                    if (reg.GetValue(reg_FormWidth) == null) throw new Exception(__ERR1_fail_write_registry + reg.Name);
                    int fWidth = System.Convert.ToInt32(reg.GetValue(reg_FormWidth));
                    if (reg.GetValue(reg_FormHeight) == null) throw new Exception(__ERR1_fail_write_registry + reg.Name);
                    int fHeight = System.Convert.ToInt32(reg.GetValue(reg_FormHeight));
                    frm.Top = fTop;
                    frm.Left = fLeft;
                    frm.Width = fWidth;
                    frm.Height = fHeight;
                }               
            }
            catch (Exception e){}
            return;
        }
	}
	
}
