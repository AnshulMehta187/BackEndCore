using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Utilties
{
  public partial class Enums
  {
    public enum MenuJobContactRole
    {
      [Description("Body Corporate Manager")]
      BodyCorporateManager = 1,
      [Description("Building Owner")]
      BuildingOwner,
      [Description("Building Owners Representative")]
      BuildingOwnersRepresentative,
      [Description("Real Estate Agent")]
      RealEstateAgent,
      [Description("Tenant")]
      Tenant,
      [Description("Tenants Adjuster")]
      TenantsAdjuster,
      [Description("Tenants Representative")]
      TenantsRepresentative,
      [Description("Adjuster")]
      Adjuster,
      [Description("Architect")]
      Architect,
      [Description("Broker")]
      Broker,
      [Description("Claims Manager")]
      ClaimsManager,
      [Description("Claims Preparer")]
      ClaimsPreparer,
      [Description("Client - Primary")]
      ClientPrimary,
      [Description("Client - Secondary")]
      ClientSecondary,
      [Description("Insurance Contact")]
      InsuranceContact,
      [Description("Originator")]
      Originator,
      [Description("Solicitor")]
      Solicitor,
      [Description("Sub-Contractor")]
      SubContractor,
      [Description("Superintendent")]
      Superintendent,
      [Description("Council Contact")]
      CouncilContact,
      [Description("Third Party")]
      ThirdParty,
      [Description("Consultant Company")]
      ConsultantCompany,
      [Description("Owners Corporation Representative")]
      OwnersCorporationRepresentative,
      [Description("Invoice Recipient")]
      InvoiceRecipient,
      [Description("Site Contact")]
      SiteContact
    }

  }

}
