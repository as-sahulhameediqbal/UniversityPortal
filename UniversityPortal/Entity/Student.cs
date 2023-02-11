﻿namespace UniversityPortal.Entity
{
    public class Student : EntityBase
    {
        public string Name { get; set; } = null!;
        public string AdmissionNumber { get; set; } = null!;
        public string RollNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid UniversityId { get; set; }
        public Guid UserId { get; set; }
        public string Program { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTimeOffset JoiningDate { get; set; }
        public decimal TutionFee { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }

    }
}
