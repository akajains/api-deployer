using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHub.Domain.Entities
{
    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            //EmployeePositions = new HashSet<EmployeePosition>();
        }

        [Column("empNo")]
        [Key]
        [Required]
        [StringLength(50)]
        public string Id { get; set; }

        [Column("userID")]
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column("fName")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Column("lName")]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Column("middleName")]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [Column("preferredName")]
        [StringLength(50)]
        public string PreferredName { get; set; }

        [StringLength(511)]
        public string FullName { get; set; }
        //public string FullName => ((PreferredName ?? FirstName) + " " + LastName).Trim();

        [Column("mainWorkAddressStreet")]
        [StringLength(255)]
        public string WorkStreetHouseNo { get; set; }

        [Column("mainWorkAddressPostCode")]
        [StringLength(255)]
        public string WorkPostcode { get; set; }

        [Column("mainWorkAddresssuburbCity")]
        [StringLength(255)]
        public string WorkSuburb { get; set; }

        [Column("mainWorkAddressStateTerritory")]
        [StringLength(255)]
        public string WorkState { get; set; }

        [Column("mainWorkAddressCountry")]
        [StringLength(255)]
        public string WorkCountryCode { get; set; }

        [Column("nominatedMailingAddressStreet")]
        [StringLength(255)]
        public string WorkMailingStreetHouseNo { get; set; }

        [Column("nominatedMailingAddressPostCode")]
        [StringLength(255)]
        public string WorkMailingPostcode { get; set; }

        [Column("nominatedMailingAddressSuburbCity")]
        [StringLength(255)]
        public string WorkMailingSuburb { get; set; }

        [Column("nominatedMailingAddressStateTerritory")]
        [StringLength(255)]
        public string WorkMailingState { get; set; }

        [Column("nominatedMailingAddressCountry")]
        [StringLength(255)]
        public string WorkMailingCountryCode { get; set; }

        [Column("telephoneNo")]
        [StringLength(50)]
        public string WorkTelephoneNo { get; set; }

        [Column("mobileNo")]
        [StringLength(50)]
        public string WorkMobileNo { get; set; }

        [Column("emailAddress")]
        [StringLength(255)]
        public string WorkEmailAddress { get; set; }

        [Column("homeTelephoneNo")]
        [StringLength(50)]
        public string HomeTelephoneNo { get; set; }

        [Column("homeMobileNo")]
        [StringLength(50)]
        public string HomeMobileNo { get; set; }

        [Column("businessUnit")]
        [StringLength(50)]
        public string BusinessUnit { get; set; }

        [Column("personnelArea")]
        [StringLength(50)]
        public string WorkLocation { get; set; }

        [Column("personnelSubArea")]
        [StringLength(50)]
        public string WorkSubLocation { get; set; }

        [Column("empGroup")]
        [StringLength(50)]
        public string Group { get; set; }

        [Column("empSubGroup")]
        [StringLength(50)]
        public string SubGroup { get; set; }

        [Column("contractType")]
        [StringLength(50)]
        public string ContractType { get; set; }

        [Column("employmentInstrument")]
        [StringLength(50)]
        public string TimesheetScenarioCode { get; set; }

        [Column("costCentre")]
        [StringLength(50)]
        public string CostCentre { get; set; }

        [Column("OneUpEmployeeNo")]
        [StringLength(50)]
        public string ManagerId { get; set; }

        [Column("dateCommencedInPosition")]
        public DateTime? PositionStartDate { get; set; }

        [Column("positionNo")]
        [StringLength(50)]
        public string PositionId { get; set; }

        [Column("positionTitle")]
        [StringLength(50)]
        public string PositionTitle { get; set; }

        [Column("hrOrgUnit")]
        [StringLength(255)]
        public string OrgUnitId { get; set; }

        [Column("jobCode")]
        [StringLength(255)]
        public string JobCodeId { get; set; }

        [Column("workCode")]
        [StringLength(255)]
        public string WorkCode { get; set; }

        [Column("jobTitle")]
        [StringLength(255)]
        public string JobTitle { get; set; }

        [Column("band")]
        [StringLength(255)]
        public string Band { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual OrgUnit OrgUnit { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }
    }
}
