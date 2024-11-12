using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace gestiondette.entities
{
    public abstract class AbstractEntity
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreateAt { get; set; }


        public DateTime UpdateAt { get; set; }

        // Association Many-to-One pour UserCreate
        [NotMapped]
        public User UserCreate { get; set; }




        [NotMapped]
        public User UserUpdate { get; set; }


        public void OnPrePersist()
        {
            CreateAt = DateTime.UtcNow;

            Console.WriteLine($"CreateAt: {CreateAt}, UpdateAt: {UpdateAt}");
        }

        public void OnPreUpdate()
        {
            UpdateAt = DateTime.Now;
        }
    }
}
