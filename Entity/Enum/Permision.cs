using System.ComponentModel.DataAnnotations;

namespace Entity.Enum
{
    public enum Permision
    {
        [Display(Name = "Create")]
        Create = 1,

        [Display(Name = "Read")]
        Read = 2,

        [Display(Name = "Update")]
        Update = 3,

        [Display(Name = "Delete")]
        Delete = 4,

    }
}
