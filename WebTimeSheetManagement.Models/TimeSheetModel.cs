namespace WebTimeSheetManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="TimeSheetModel" />
    /// </summary>
    [NotMapped]
    public class TimeSheetModel
    {
        /// <summary>
        /// Gets or sets the hdtext1
        /// </summary>
        public DateTime hdtext1 { get; set; }

        /// <summary>
        /// Gets or sets the hdtext2
        /// </summary>
        public DateTime hdtext2 { get; set; }

        /// <summary>
        /// Gets or sets the hdtext3
        /// </summary>
        public DateTime hdtext3 { get; set; }

        /// <summary>
        /// Gets or sets the hdtext4
        /// </summary>
        public DateTime hdtext4 { get; set; }

        /// <summary>
        /// Gets or sets the hdtext5
        /// </summary>
        public DateTime hdtext5 { get; set; }

        /// <summary>
        /// Gets or sets the hdtext6
        /// </summary>
        public DateTime hdtext6 { get; set; }

        /// <summary>
        /// Gets or sets the hdtext7
        /// </summary>
        public DateTime hdtext7 { get; set; }

        /// <summary>
        /// Gets or sets the text1_p1
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text1_p1 { get; set; }

        /// <summary>
        /// Gets or sets the text2_p1
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text2_p1 { get; set; }

        /// <summary>
        /// Gets or sets the text3_p1
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text3_p1 { get; set; }

        /// <summary>
        /// Gets or sets the text4_p1
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text4_p1 { get; set; }

        /// <summary>
        /// Gets or sets the text5_p1
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text5_p1 { get; set; }

        /// <summary>
        /// Gets or sets the text6_p1
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text6_p1 { get; set; }

        /// <summary>
        /// Gets or sets the text7_p1
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "P0 to 24")]
        public int? text7_p1 { get; set; }

        /// <summary>
        /// Gets or sets the texttotal_p1
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? texttotal_p1 { get; set; }

        /// <summary>
        /// Gets or sets the Description_p1
        /// </summary>
        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_p1 { get; set; }

        /// <summary>
        /// Gets or sets the text1_p2
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text1_p2 { get; set; }

        /// <summary>
        /// Gets or sets the text2_p2
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text2_p2 { get; set; }

        /// <summary>
        /// Gets or sets the text3_p2
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text3_p2 { get; set; }

        /// <summary>
        /// Gets or sets the text4_p2
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text4_p2 { get; set; }

        /// <summary>
        /// Gets or sets the text5_p2
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text5_p2 { get; set; }

        /// <summary>
        /// Gets or sets the text6_p2
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text6_p2 { get; set; }

        /// <summary>
        /// Gets or sets the text7_p2
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text7_p2 { get; set; }

        /// <summary>
        /// Gets or sets the texttotal_p2
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? texttotal_p2 { get; set; }

        /// <summary>
        /// Gets or sets the Description_p2
        /// </summary>
        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_p2 { get; set; }

        /// <summary>
        /// Gets or sets the text1_p3
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text1_p3 { get; set; }

        /// <summary>
        /// Gets or sets the text2_p3
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text2_p3 { get; set; }

        /// <summary>
        /// Gets or sets the text3_p3
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text3_p3 { get; set; }

        /// <summary>
        /// Gets or sets the text4_p3
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text4_p3 { get; set; }

        /// <summary>
        /// Gets or sets the text5_p3
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text5_p3 { get; set; }

        /// <summary>
        /// Gets or sets the text6_p3
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text6_p3 { get; set; }

        /// <summary>
        /// Gets or sets the text7_p3
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text7_p3 { get; set; }

        /// <summary>
        /// Gets or sets the texttotal_p3
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? texttotal_p3 { get; set; }

        /// <summary>
        /// Gets or sets the Description_p3
        /// </summary>
        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_p3 { get; set; }

        /// <summary>
        /// Gets or sets the text1_p4
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text1_p4 { get; set; }

        /// <summary>
        /// Gets or sets the text2_p4
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text2_p4 { get; set; }

        /// <summary>
        /// Gets or sets the text3_p4
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text3_p4 { get; set; }

        /// <summary>
        /// Gets or sets the text4_p4
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text4_p4 { get; set; }

        /// <summary>
        /// Gets or sets the text5_p4
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text5_p4 { get; set; }

        /// <summary>
        /// Gets or sets the text6_p4
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text6_p4 { get; set; }

        /// <summary>
        /// Gets or sets the text7_p4
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text7_p4 { get; set; }

        /// <summary>
        /// Gets or sets the texttotal_p4
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? texttotal_p4 { get; set; }

        /// <summary>
        /// Gets or sets the Description_p4
        /// </summary>
        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_p4 { get; set; }

        /// <summary>
        /// Gets or sets the text1_p5
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text1_p5 { get; set; }

        /// <summary>
        /// Gets or sets the text2_p5
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text2_p5 { get; set; }

        /// <summary>
        /// Gets or sets the text3_p5
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text3_p5 { get; set; }

        /// <summary>
        /// Gets or sets the text4_p5
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text4_p5 { get; set; }

        /// <summary>
        /// Gets or sets the text5_p5
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text5_p5 { get; set; }

        /// <summary>
        /// Gets or sets the text6_p5
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text6_p5 { get; set; }

        /// <summary>
        /// Gets or sets the text7_p5
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text7_p5 { get; set; }

        /// <summary>
        /// Gets or sets the texttotal_p5
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? texttotal_p5 { get; set; }

        /// <summary>
        /// Gets or sets the Description_p5
        /// </summary>
        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_p5 { get; set; }

        /// <summary>
        /// Gets or sets the text1_p6
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text1_p6 { get; set; }

        /// <summary>
        /// Gets or sets the text2_p6
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text2_p6 { get; set; }

        /// <summary>
        /// Gets or sets the text3_p6
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text3_p6 { get; set; }

        /// <summary>
        /// Gets or sets the text4_p6
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text4_p6 { get; set; }

        /// <summary>
        /// Gets or sets the text5_p6
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text5_p6 { get; set; }

        /// <summary>
        /// Gets or sets the text6_p6
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text6_p6 { get; set; }

        /// <summary>
        /// Gets or sets the text7_p6
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? text7_p6 { get; set; }

        /// <summary>
        /// Gets or sets the texttotal_p6
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? texttotal_p6 { get; set; }

        /// <summary>
        /// Gets or sets the Description_p6
        /// </summary>
        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_p6 { get; set; }

        /// <summary>
        /// Gets or sets the DaysofWeek1
        /// </summary>
        public string DaysofWeek1 { get; set; }

        /// <summary>
        /// Gets or sets the DaysofWeek2
        /// </summary>
        public string DaysofWeek2 { get; set; }

        /// <summary>
        /// Gets or sets the DaysofWeek3
        /// </summary>
        public string DaysofWeek3 { get; set; }

        /// <summary>
        /// Gets or sets the DaysofWeek4
        /// </summary>
        public string DaysofWeek4 { get; set; }

        /// <summary>
        /// Gets or sets the DaysofWeek5
        /// </summary>
        public string DaysofWeek5 { get; set; }

        /// <summary>
        /// Gets or sets the DaysofWeek6
        /// </summary>
        public string DaysofWeek6 { get; set; }

        /// <summary>
        /// Gets or sets the DaysofWeek7
        /// </summary>
        public string DaysofWeek7 { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID1
        /// </summary>
        [Required(ErrorMessage = "Choose Project")]
        public int? ProjectID1 { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID2
        /// </summary>
        public int? ProjectID2 { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID3
        /// </summary>
        public int? ProjectID3 { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID4
        /// </summary>
        public int? ProjectID4 { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID5
        /// </summary>
        public int? ProjectID5 { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID6
        /// </summary>
        public int? ProjectID6 { get; set; }

        /// <summary>
        /// Gets or sets the ProjectID7
        /// </summary>
        public int? ProjectID7 { get; set; }
    }
}
