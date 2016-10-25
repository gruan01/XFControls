using System.Windows.Input;

namespace AsNum.XFControls {

    /// <summary>
    /// 可选中数据接口, 用于 ViewModel 
    /// </summary>
    public interface ISelectable {

        /// <summary>
        /// 是否已选中
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// 选中时，触发该命令
        /// </summary>
        ICommand SelectedCommand { get; set; }

        /// <summary>
        /// 取消选中时，触发该命令
        /// </summary>
        ICommand UnSelectedCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        void NotifyOfPropertyChange(string propertyName);
    }
}
