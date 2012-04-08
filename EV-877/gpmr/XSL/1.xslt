<?xml version="1.0" encoding="UTF-8"?>
<!-- This file is taken from the v3 Generator tool (acquired from http://gforge.hl7.org/gf/project/v3-generator/ )
     and is covered under the terms of the HL7 Basic License.
     
     All modifications to this file are noted below:
     
      * Added template for the annotations, as annotations are not rendered correctly according to the schema. For example, when rendering annotations are output as such:
        <description>
          <p>Blah blah</p>
          
          Correct way
          
        <description>
          <text>
              <p>Blah blah</p>
      * Modified this file to function under XSLT 1.0 
     -->
<!-- $Id: StaticMIF1to2.xslt 8360 2009-10-26 06:00:22Z woody_beeler $ -->
<!-- TESTED for MIF 2.1.5:
	1) Converted namespace urn for xhtml to 'urn:hl7-org:v3/mif2'  in root element
	2) Changed default value for param schemaVersion to '2.1.5'
    3) Added switch variables htmlPrefix, htmlNamespace and licenseKind so that schemaVersion controls: 
		A) Replace "{concat('html:', local-name(.))}" namespace="{$htmlNamespace}" with
			"{concat($htmlPrefix, local-name(.))}" namespace="{$htmlNamespace}" in three places
		B) Replaces 'name="html:' with 'name="{concat($htmlPrefix,-name-)}' in 15 <xsl:element declarations and retained 'namespace="{$htmlNamespace}"' in each. 
        C) Selection of license terms header from MIF2 license file that is either a single or dual namespace header.
    4) Inserted a KLUDGE at each of lines 608 and 618 to correct for absence of proper sort names and traversal names
        when a CMET is at the end of a traversableConnection or is a choice item.  These should be removed when corrected, although 
        once corrected, they will no longer be activated. 
      
 -->  
<!-- Todo: extend to support derived static models too -->
<xsl:stylesheet version="1.0" 
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxml="urn:schemas-microsoft-com:xslt"
                xmlns:msxsl="http://www.microsoft.com/msxsl"
                xmlns:mif="urn:hl7-org:v3/mif" 
                xmlns:mif2="urn:hl7-org:v3/mif2" 
                xmlns:html="http://www.w3.org/1999/xhtml" 
                exclude-result-prefixes="mif2 msxml">
  <xsl:include href="ProcessDefinitionForAnnotationNodes.xsl"/>
  <!--<xsl:include href="DocumentationFix.xslt"/>-->
  <!--   <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes" omit-xml-declaration="yes"/>
   gwb - reverted to previous because seemed to affect some characters.  Does this hurt LM? -->
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <!--
    - Initiate random generator based on current time to ensure truly random numbers
    -->
  <xsl:param name="parseAnnotations" select="'false'"/>
  <!--<xsl:param name="licenseFile"/>-->
  <xsl:param name="currentYear" select="'2010'"/>
  <xsl:param name="schemaVersion" select="'2.1.4'"/>
  <xsl:variable name="singleNamespace">
	  <xsl:choose>
				<xsl:when test="$schemaVersion>'2.1.4'"><xsl:value-of select="'single'"/></xsl:when>
		<xsl:otherwise><xsl:value-of select="'double'"/></xsl:otherwise>
	</xsl:choose>
  </xsl:variable>
  <xsl:variable name="licenseKind">
	  <xsl:choose>
				<xsl:when test="$singleNamespace='single'"><xsl:value-of select="'MIF215'"/></xsl:when>
		<xsl:otherwise><xsl:value-of select="'MIF2'"/></xsl:otherwise>
	</xsl:choose>
  </xsl:variable>
  <!--<xsl:variable name="licenseHeader" select="document($licenseFile)/mif2:licenseHeader"/>-->
  <!--<xsl:variable name="randomGenerator" select="random:new(date:getTime(date:new()))"/>-->
  <xsl:variable name="mifNamespace" select="'urn:hl7-org:v3/mif2'"/>
  <xsl:variable name="htmlNamespace" select="'http://www.w3.org/1999/xhtml'">
  </xsl:variable> 
  <xsl:variable name="htmlPrefix" select="'html:'"/>
  <xsl:variable name="realm">
    <xsl:choose>
      <xsl:when test="//mif:packageLocation[1]/@realm">
        <xsl:value-of select="//mif:packageLocation[1]/@realm"/>
      </xsl:when>
      <xsl:otherwise>UV</xsl:otherwise>
    </xsl:choose>
  </xsl:variable>
  <xsl:template match="mif:description[not(./mif:text)]|mif:walkthrough[not(./mif:text)]">
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
      <mif:text>
        <xsl:for-each select="*">
          <xsl:apply-templates select="." mode="html"/>
        </xsl:for-each>
      </mif:text>
    </xsl:element>
  </xsl:template>
  
  
  <xsl:template match="/">
    <xsl:choose>
      <xsl:when test="*/@schemaVersion">
        <xsl:copy-of select="."/>
        <xsl:message>This model is already MIF2 - no conversion performed</xsl:message>
      </xsl:when>
      <xsl:otherwise>
			<!--<xsl:if test="$licenseHeader/@representationKind='MIF2'">
				<xsl:variable name="termsText">
					<xsl:value-of select="$licenseHeader/mif2:header[@representationKind=$licenseKind]/mif2:legalese/mif2:licenseTerms/node()[local-name(.)='code']"/>
				</xsl:variable>
				<xsl:comment>
					<xsl:value-of select="concat(substring-before($termsText, '{DATE}'), $currentYear, substring-after($termsText, '{DATE}'))"/>
				</xsl:comment>
				--><!-- Insert license terms comment at beginning of file --><!--
			</xsl:if>-->
			<xsl:apply-templates/>
        
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="comment()[following-sibling::*[not(parent::*)]]"> 
		<!-- Drop previous License assertion (any comment that precedes the root element). A new one will be added when the root element is found -->
 </xsl:template>

  
  
  <xsl:template match="@*|text()|comment()">
    <xsl:copy-of select="."/>
    
  </xsl:template>
  <xsl:template match="*">
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="text()[normalize-space(.)='']"/>
  <xsl:template match="mif:staticModel|mif:serializedStaticModel">
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
      <xsl:attribute name="conformanceLevel">
        <xsl:choose>
          <xsl:when test="mif:packageLocation/@realm='UV' or not(mif:packageLocation/@realm)">International</xsl:when>
          <xsl:when test="mif:packageLocation/@realm='ZZ'">Localization</xsl:when>
          <xsl:otherwise>RealmExtension</xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:attribute name="schemaVersion"><xsl:value-of select="$schemaVersion"/></xsl:attribute>
      <xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="node()[not(self::mif:context)]"/>
      <!-- in reverse, drop replacedBy -->
    </xsl:element>
  </xsl:template>
  <xsl:template match="@packageKind">
    <xsl:attribute name="packageKind">
      <xsl:choose>
        <xsl:when test=".='realm'">realmNamespace</xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="."/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:attribute>
  </xsl:template>
  <xsl:template match="mif:historyItem/mif:description/*|mif:voterComments/*|mif:annotations/*[not(self::mif:staticExample)]/mif:text/*|mif:existingContent/*|mif:suggestedReplacement/*|mif:resolutionComments/*">
    <xsl:apply-templates mode="html" select="."/>
  </xsl:template>
  <xsl:template match="mif:context">
    <xsl:element name="mif:realmNamespace" namespace="{$mifNamespace}">
      <xsl:apply-templates select="node()|@*"/>
      <!-- in reverse, strip cascadeInfo for business names -->
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:header">
    <xsl:element name="mif:header" namespace="{$mifNamespace}">
	  <xsl:choose>
			<xsl:when test="mif:legalese">
			  <xsl:apply-templates select="*"/>
			  <xsl:apply-templates select="parent::*/mif:context"/>	
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="mif:renderingInformation"/>
				<!-- add leghalese -->
				<xsl:element name="mif:legalese" namespace="{$mifNamespace}">
					<xsl:attribute name="copyrightOwner"><xsl:value-of select="'Health Level Seven'"/></xsl:attribute>
					<xsl:attribute name="copyrightYears"><xsl:value-of select="$currentYear"/></xsl:attribute>
					<!--<xsl:copy-of select="$licenseHeader/mif2:header[@representationKind=$licenseKind]/mif:legalese/mif2:notation"/>-->
					<xsl:element name="mif:licenseTerms" namespace="{$mifNamespace}">
						<!--<xsl:copy-of select="$licenseHeader/mif2:header[@representationKind=$licenseKind]/mif2:legalese/mif2:licenseTerms/*[not(self::node()[local-name(.)='code'])]"/>-->
					</xsl:element>
				</xsl:element>
				<xsl:apply-templates select="*[local-name(.)!='renderingInformation']"/>
			    <xsl:apply-templates select="parent::*/mif:context"/>	
			</xsl:otherwise>
	  </xsl:choose>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:legalese">
		<xsl:element name="mif:legalese" namespace="{$mifNamespace}">
			<xsl:apply-templates select="@*| mif:disclaimer| mif:notation"/>
			<xsl:if test="not(mif:notation)">
				<!--<xsl:copy-of select="$licenseHeader/mif2:header[@representationKind=$licenseKind]/mif:legalese/mif2:notation"/>-->
			</xsl:if>
			<xsl:element name="mif:licenseTerms" namespace="{$mifNamespace}">
				<!--<xsl:copy-of select="$licenseHeader/mif2:header[@representationKind=$licenseKind]/mif2:legalese/mif2:licenseTerms/*[not(self::node()[local-name(.)='code'])]"/>-->
			</xsl:element>
		</xsl:element>  
  </xsl:template>
  <xsl:template match="mif:packageLocation|mif:mapping/mif:sourceArtifact|mif:importedDatatypeModelPackage|mif:importedCommonModelElementPackage|mif:importedStubPackage|mif:targetStaticModel|mif:importedVocabularyModelPackage">
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
      <!--      <xsl:attribute name="combinedId">
        <xsl:value-of select="concat(@root, '=')"/>
        <xsl:if test="@domain">
          <xsl:value-of select="concat(@domain, '=')"/>
        </xsl:if>
        <xsl:choose>
          <xsl:when test="@creatorId">
            <xsl:value-of select="@creatorId"/>
          </xsl:when>
          <xsl:when test="@realm">
            <xsl:value-of select="@realm"/>
          </xsl:when>
          <xsl:otherwise>UV</xsl:otherwise>
        </xsl:choose>
        <xsl:text>=</xsl:text>
        <xsl:choose>
          <xsl:when test="contains(@artifact, '-deprecated')">
            <xsl:value-of select="substring-before(@artifact, '-deprecated')"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="@artifact"/>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:if test="@id">
          <xsl:value-of select="concat('=', @id)"/>
        </xsl:if>
        <xsl:if test="@version">
          <xsl:value-of select="concat('=', @version)"/>
        </xsl:if>
      </xsl:attribute>-->
      <xsl:if test="@realm or @creatorId">
        <xsl:attribute name="realmNamespace">
          <xsl:choose>
            <xsl:when test="@creatorId">
              <xsl:value-of select="@creatorId"/>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="@realm"/>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:attribute>
      </xsl:if>
      <xsl:attribute name="artifact">
        <xsl:choose>
          <xsl:when test="contains(@artifact, '-deprecated')">
            <xsl:value-of select="substring-before(@artifact, '-deprecated')"/>
          </xsl:when>
          <xsl:when test="@artifact='CMET'">IFC</xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="@artifact"/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>
      <xsl:apply-templates select="@*[not(name(.)='realm' or name(.)='creatorId' or name(.)='artifact')]"/>
      <!-- In reverse, trim off releaseDate -->
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:ballotInfo">
    <xsl:element name="mif:approvalInfo" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="node()"/>
      <!-- Drop approvingOrganization -->
    </xsl:element>
  </xsl:template>
  <xsl:template match="@ballotStatus">
    <xsl:attribute name="approvalStatus">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>
  <xsl:template match="@ballotDate">
    <xsl:attribute name="approvalDate">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>
  <xsl:template match="mif:renderingInformation/@schemaVersion"/>
  <xsl:template match="mif:attribute"> <!-- Process Attributes for Properties contained in Annotations -->
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="node()"/>
      <xsl:if test="$schemaVersion!='2.1.2' and $parseAnnotations='true' and mif:annotations/mif:definition/mif:text[mif:p/node()[local-name()='i' or local-name()='b'][contains(text(),':')]]">
        <xsl:call-template name="processForAnnotations">
          <xsl:with-param name="sourceNodes" select="mif:annotations/mif:definition/mif:text/*"/>
          <xsl:with-param name="pickUpProperties" select="'T'"/>
        </xsl:call-template> 
      </xsl:if>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:annotations">
    <xsl:choose>
      <xsl:when test="$parseAnnotations='true' and mif:definition/mif:text[mif:p/node()[local-name()='i' or local-name()='b'][contains(text(),':')]]">
        <xsl:call-template name="processForAnnotations">
          <xsl:with-param name="sourceNodes" select="mif:definition/mif:text/*"/>
        </xsl:call-template> 
      </xsl:when>
      <xsl:otherwise>
        <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
          <xsl:if test="mif:definition|mif:description|mif:usageNotes|mif:rationale|mif:designComments|mif:walkthrough|mif:appendix|mif:otherAnnotation">
            <xsl:element name="mif:documentation" namespace="{$mifNamespace}">
              <xsl:apply-templates select="mif:definition"/>
              <xsl:apply-templates select="mif:description"/>
              <xsl:apply-templates select="mif:usageNotes"/>
              <xsl:apply-templates select="mif:rationale"/>
              <xsl:apply-templates select="mif:designComments"/>
              <xsl:apply-templates select="mif:walkthrough"/>
              <xsl:apply-templates select="mif:appendix"/>
              <xsl:apply-templates select="mif:otherAnnotation"/>
            </xsl:element>
          </xsl:if>
          <xsl:if test="mif:mapping|mif:constraint|mif:openIssues|mif:staticExample|mif:ballotComment">
            <xsl:element name="mif:appInfo" namespace="{$mifNamespace}">
              <xsl:apply-templates select="mif:mapping"/>
              <xsl:apply-templates select="mif:constraint"/>
              <xsl:apply-templates select="mif:openIssues"/>
              <xsl:apply-templates select="mif:staticExample"/>
              <xsl:apply-templates select="mif:ballotComment"/>
            </xsl:element>
          </xsl:if>
        </xsl:element>
        <!-- in reverse, for all annotations, toast prependAnnotationId, appendAnnotationId, combinedText, cascadeInfo -->
        <!-- in reverse, drop requirements, changeRequest, deprecationInfo -->
        <!-- in reverse, drop openIssues/resolution -->
        <!-- in reverse, drop ballotComment/@implementedDate;@implementingPersonName -->
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="mif:openIssues">
    <xsl:element name="mif:openIssue" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*|node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:constraint">
    <xsl:element name="mif:formalConstraint" namespace="{$mifNamespace}">
      <xsl:if test="mif:graphicRepresentation">
        <xsl:attribute name="graphicLinkId">
          <xsl:call-template name="getGUID"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:usageNotes|mif:requirements|mif:walkthrough|mif:otherAnnotation|mif:deprecationInfo|mif:appendix|mif:staticExample">
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:historyItem">
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*[not(name(.)='modifiedForPackageVersion')]"/>
      <xsl:apply-templates select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="*[mif:graphicRepresentation and not(self::mif:staticModel or self::mif:serializedStaticModel or self::mif:ownedSubjectAreaPackage or self::mif:constraint or self::mif:description)]">
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
        <xsl:attribute name="graphicLinkId">
          <xsl:call-template name="getGUID"/>
        </xsl:attribute>
        <xsl:apply-templates select="@*"/>
        <xsl:apply-templates select="node()"/>
      </xsl:element>
  </xsl:template>
  <xsl:template match="@containerDiagramName"/>
  <xsl:template name="handleFigure">
    <xsl:param name="namespace"/>
    <xsl:choose>
      <xsl:when test="mif:resizable">
        <xsl:for-each select="mif:resizable">
          <xsl:copy-of select="@imageType"/>
          <xsl:attribute name="src">
            <xsl:value-of select="@href"/>
          </xsl:attribute>
          <xsl:attribute name="alt">
            <xsl:choose>
              <xsl:when test="parent::*/parent::mif:subjectArea">Class diagram of subject area</xsl:when>
              <xsl:otherwise>Class diagram of static model</xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:if test="@recommendedHeight">
            <xsl:attribute name="height">
              <xsl:value-of select="@recommendedHeight"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="@recommendedWeight">
            <xsl:attribute name="weight">
              <xsl:value-of select="@recommendedWeight"/>
            </xsl:attribute>
          </xsl:if>
        </xsl:for-each>
      </xsl:when>
      <xsl:when test="mif:fixedImage">
        <xsl:for-each select="mif:fixedImage">
          <xsl:attribute name="imageType">
            <xsl:choose>
              <xsl:when test="contains(mif:pixmap/@href, '.ps')">application/postscript</xsl:when>
              <xsl:when test="contains(mif:pixmap/@href, '.pdf')">application/pdf</xsl:when>
              <xsl:when test="contains(mif:pixmap/@href, '.png')">image/png</xsl:when>
              <xsl:when test="contains(mif:pixmap/@href, '.svg')">image/svg+xml</xsl:when>
              <xsl:when test="contains(mif:pixmap/@href, '.jpg')">image/jpeg</xsl:when>
              <xsl:when test="contains(mif:pixmap/@href, '.gif')">image/gif</xsl:when>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="src">
            <xsl:value-of select="mif:pixmap/@href"/>
          </xsl:attribute>
          <xsl:attribute name="alt">
            <xsl:choose>
              <xsl:when test="parent::*/parent::mif:subjectArea">Class diagram of subject area</xsl:when>
              <xsl:otherwise>Class diagram of static model</xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:copy-of select="mif:pixmap/@height|mif:pixmap/@weight"/>
          <xsl:for-each select="mif:thumbnail">
            <xsl:element name="mif:thumbnail" namespace="{$namespace}">
              <xsl:attribute name="imageType">
                <xsl:choose>
                  <xsl:when test="contains(@href, '.ps')">application/postscript</xsl:when>
                  <xsl:when test="contains(@href, '.pdf')">application/pdf</xsl:when>
                  <xsl:when test="contains(@href, '.png')">image/png</xsl:when>
                  <xsl:when test="contains(@href, '.svg')">image/svg+xml</xsl:when>
                  <xsl:when test="contains(@href, '.jpg')">image/jpeg</xsl:when>
                  <xsl:when test="contains(mif:pixmap/@href, '.gif')">image/gif</xsl:when>
                </xsl:choose>
              </xsl:attribute>
              <xsl:attribute name="src">
                <xsl:value-of select="@href"/>
              </xsl:attribute>
              <xsl:attribute name="alt">
                <xsl:choose>
                  <xsl:when test="parent::*/parent::*/parent::mif:subjectArea">Class diagram of subject area</xsl:when>
                  <xsl:otherwise>Class diagram of static model</xsl:otherwise>
                </xsl:choose>
              </xsl:attribute>
              <xsl:copy-of select="@height|@weight"/>
            </xsl:element>
          </xsl:for-each>
        </xsl:for-each>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="mif:figure">
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
      <xsl:call-template name="handleFigure">
        <xsl:with-param name="namespace" select="$mifNamespace"/>
      </xsl:call-template>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:derivationSupplier/@sortKey"/>
  <xsl:template match="mif:derivationSupplier">
    <xsl:element name="mif:derivedFrom" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*|node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:derivationClient">
    <!-- This is derivable, so we won't bring it across -->
  </xsl:template>
  <xsl:template match="mif:ownedSubjectAreaPackage">
    <xsl:element name="mif:subjectAreaPackage" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*|node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:ownedEntryPoint" priority="5">
    <xsl:element name="mif:entryPoint" namespace="{$mifNamespace}">
      <xsl:if test="mif:graphicRepresentation">
        <xsl:attribute name="graphicLinkId">
          <xsl:call-template name="getGUID"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="not(@name)">
        <xsl:attribute name="name">
          <xsl:value-of select="@className|mif:ownedClass/*/@name"/>
        </xsl:attribute> 
      </xsl:if>
      <xsl:apply-templates select="@*[not(name(.)='isAbstract')]|node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:ownedEntryPoint/mif:specializedClass" priority="5">
    <xsl:element name="mif:entryClass" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*|node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:ownedClass">
    <xsl:element name="mif:containedClass" namespace="{$mifNamespace}">
      <xsl:apply-templates select="@*|node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="@isAbstract[parent::mif:templateParameter or parent::mif:commonModelElementRef]"/>
  <xsl:template match="@deprecatedFixedName|@deprecatedLegacyName|@deprecatedNameExtension|@deprecatedNameStatus"/>
  <xsl:template match="mif:supplierStructuralDomain">
    <xsl:element name="mif:definingVocabulary" namespace="{$mifNamespace}">
      <xsl:call-template name="handleVocabulary"/>
    </xsl:element>
  </xsl:template>
  <xsl:template name="handleVocabulary">
    <xsl:choose>
      <xsl:when test="@domainName">
		 <xsl:choose> <!-- Added to permit a parameter to signal conversion to MIF 2.1.2 for the SMD tool that uses this schema 
									Not sure the following changes are sufficient for the "full" MIF, but they will permit a valid 2.1.2 LITE MIF -->
			<xsl:when test="$schemaVersion='2.1.2'">
				<xsl:copy-of select="@codingStrength"/>
				<xsl:element name="mif:vocabularyDomain" namespace="{$mifNamespace}">
          <xsl:attribute name="name">
            <xsl:value-of select="@domainName"/>
          </xsl:attribute>
				</xsl:element>
			</xsl:when>
			<xsl:otherwise>
				<xsl:element name="mif:conceptDomain" namespace="{$mifNamespace}">
          <xsl:attribute name="name">
            <xsl:value-of select="@domainName"/>
          </xsl:attribute>
				</xsl:element>
			</xsl:otherwise>
		</xsl:choose>
      </xsl:when>
<!--      <xsl:when test="@valueSet"> gwb 20090321 WHAT is this?  The element is supplierDomainSpecification, and the attribute is "valueSetName" !!!!! -->
      <xsl:when test="@valueSetName">
<!-- gwb 20090321 WHAT is this?  The element is supplierDomainSpecification, and the attribute is "valueSetName" !!!!! -->
        <xsl:variable name="baseValueSet">
          <xsl:choose>
            <xsl:when test="contains(@valueSetName, '#')">
              <xsl:value-of select="substring-before(@valueSetName, '#')"/>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="@valueSetName"/>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:variable>
		<xsl:if test="$schemaVersion='2.1.2'">
          <xsl:copy-of select="@codingStrength"/>
		</xsl:if>
        <xsl:element name="mif:valueSet" namespace="{$mifNamespace}">
          <xsl:choose>
            <xsl:when test="substring($baseValueSet,1,2)='1.'">
              <xsl:attribute name="id">
                <xsl:value-of select="$baseValueSet"/>
              </xsl:attribute>
            </xsl:when>
            <xsl:otherwise>
              <xsl:attribute name="name">
                <xsl:value-of select="$baseValueSet"/>
              </xsl:attribute>
            </xsl:otherwise>
          </xsl:choose>
          <xsl:if test="contains(@valueSetName, '#')">
            <xsl:attribute name="valueSetVersion">
              <xsl:value-of select="substring-after(@valueSetName, '#')"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="$schemaVersion!='2.1.2'">
            <xsl:if test="@mnemonic">
              <xsl:attribute name="rootCode" >
                <xsl:value-of select="@mnemonic"/>
              </xsl:attribute>
            </xsl:if>
            <xsl:copy-of select="@codingStrength"/>
          </xsl:if>
        </xsl:element>
      </xsl:when>
      <xsl:otherwise>
		<xsl:if test="$schemaVersion='2.1.2'">
          <xsl:copy-of select="@codingStrength"/>
		</xsl:if>
        <xsl:element name="mif:code" namespace="{$mifNamespace}">
          <xsl:choose>
            <xsl:when test="not(@codeSystemName)">
              <xsl:attribute name="codeSystemName">
                <xsl:apply-templates mode="findBaseClassName" select="ancestor::mif:class[1]"/>
                <xsl:text>Class</xsl:text>
              </xsl:attribute>
            </xsl:when>
            <xsl:when test="substring(@codeSystemName, 1,2)='1.'">
              <xsl:attribute name="codeSystem" >
                <xsl:value-of select="@codeSystemName"/>
              </xsl:attribute>
            </xsl:when>
            <xsl:otherwise>
              <xsl:attribute name="codeSystemName">
                <xsl:value-of select="@codeSystemName"/>
              </xsl:attribute>
            </xsl:otherwise>
          </xsl:choose>
          <xsl:if test="@mnemonic">
            <xsl:attribute name="code">
              <xsl:value-of  select="@mnemonic"/>
            </xsl:attribute>
          </xsl:if>
        </xsl:element>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="@supplierStateAttributeName">
    <xsl:attribute name="stateAttributeName">
      <xsl:value-of  select="."/>
    </xsl:attribute>
  </xsl:template>
  <xsl:template match="mif:attribute/@referenceHistory"/>
  <xsl:template match="@isStructural">
    <xsl:attribute name="isImmutable">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>
  <xsl:template match="mif:type[parent::mif:attribute/@referenceHistory='true']">
    <xsl:element name="mif:type" namespace="{$mifNamespace}">
      <xsl:attribute name="name">HIST</xsl:attribute>
      <xsl:element name="mif:argumentDatatype" namespace="{$mifNamespace}">
        <xsl:apply-templates select="node()|@*"/>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:supplierBindingArgumentDatatype">
    <xsl:element name="mif:argumentDatatype" namespace="{$mifNamespace}">
      <xsl:apply-templates select="node()|@*"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:supplierDomainSpecification">
    <xsl:element name="mif:vocabulary" namespace="{$mifNamespace}">
      <xsl:call-template name="handleVocabulary"/>
    </xsl:element>
  </xsl:template>
  <!-- Todo: figure out what to do with datatype flavors -->
  <xsl:template match="mif:specializationChild" priority="5">
    <xsl:element name="mif:childClass" namespace="{$mifNamespace}">
      <xsl:if test="mif:graphicRepresentation">
        <xsl:attribute name="graphicLinkId">
          <xsl:call-template name="getGUID"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:attribute name="sortKey">
        <xsl:number format="000"/>
      </xsl:attribute>
      <xsl:apply-templates select="node()|@*[not(name(.)='isMandatory')]"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:specializationChild/@childClassName">
    <xsl:attribute name="name">
      <xsl:value-of select="."/>
    </xsl:attribute>
  </xsl:template>
  <xsl:template match="mif:commonModelElementRef" priority="5">
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
      <xsl:if test="mif:graphicRepresentation">
        <xsl:attribute name="graphicLinkId">
          <xsl:call-template name="getGUID"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:apply-templates select="@*"/>
      <xsl:attribute name="cmetName">
        <xsl:value-of select="@name"/>
      </xsl:attribute>
      <xsl:apply-templates select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:commonModelElementRef/mif:generalizationParent"/>
  <xsl:template match="mif:ownedAssociation" priority="5">
    <xsl:element name="mif:association" namespace="{$mifNamespace}">
      <xsl:if test="mif:graphicRepresentation">
        <xsl:attribute name="graphicLinkId">
          <xsl:call-template name="getGUID"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:connections">
    <xsl:apply-templates select="node()"/>
  </xsl:template>
  <xsl:template match="mif:traversableConnection[not(@sortKey)]"> <!-- KLUDGE UNTIL we fix fact that Cmet traversals are not generating sort keys -->
    <xsl:element name="{concat('mif:', local-name(.))}" namespace="{$mifNamespace}">
	  <xsl:attribute name="sortKey"><xsl:value-of select="'Z'"/></xsl:attribute>
      <xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="node()"/>
    </xsl:element>  
  </xsl:template>
  <xsl:template match="mif:participantClassSpecialization|mif:participantClassSpecialization//mif:specialization"> <xsl:element name="mif:choiceItem" namespace="{$mifNamespace}">
      <xsl:choose>
				<xsl:when test="@traversalName='1'"><!-- KLUDGE UNTIL we get a proper traversalName for choiceItems that go to a Cmet -->
      <xsl:apply-templates select="@className|node()"/>
    </xsl:when>
  <xsl:otherwise>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:otherwise>
		</xsl:choose>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="doGraphics" match="@*|text()|comment()">
    <xsl:copy-of select="."/>
  </xsl:template>
  <xsl:template mode="doGraphics" match="mif2:*">
    <xsl:element name="{name(.)}" namespace="{$mifNamespace}">
      <xsl:apply-templates mode="doGraphics" select="@*"/>
      <xsl:apply-templates mode="doGraphics" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="doGraphics" match="mif2:graphicRepresentation">
    <xsl:if test="parent::mif2:staticModel or parent::mif2:serializedStaticModel">
      <xsl:element name="{name(.)}" namespace="{$mifNamespace}">
        <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
        <xsl:for-each select="parent::mif2:staticModel">
          <xsl:for-each select="mif2:entryPoint/mif2:graphicRepresentation">
            <xsl:element name="mif:entryPoint" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of  select="."/>
                </xsl:attribute>
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
          <xsl:for-each select="mif2:containedClass/*/mif2:graphicRepresentation">
            <xsl:element name="mif:class" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of select="."/>
                </xsl:attribute>
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
          <xsl:for-each select="mif2:association/mif2:graphicRepresentation">
            <xsl:element name="mif:association" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of select="."/>
                </xsl:attribute>
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
          <xsl:for-each select="mif2:containedClass/mif2:childClass/mif2:graphicRepresentation">
            <xsl:element name="mif:generalization" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of select="."/>
                </xsl:attribute>
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
          <xsl:for-each select="descendant::mif2:annotations/*/mif2:graphicRepresentation">
            <xsl:element name="mif:annotation" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of  select="."/>
                </xsl:attribute>
               
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
        </xsl:for-each>
        <xsl:for-each select="parent::mif2:serializedStaticModel">
          <xsl:for-each select="mif2:entryPoint/mif2:graphicRepresentation">
            <xsl:element name="mif:entryPoint" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of select="."/>
                </xsl:attribute>
                
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
          <xsl:for-each select="descendant::*[self::mif2:specializedClass or self::mif2:participantClass]/*/mif2:graphicRepresentation">
            <xsl:element name="mif:class" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of  select="."/>
                </xsl:attribute>
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
          <xsl:for-each select="descendant::mif2:association/mif2:graphicRepresentation">
            <xsl:element name="mif:association" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of select="."/>
                </xsl:attribute>
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
          <xsl:for-each select="descendant::mif2:specializationChild/mif2:graphicRepresentation">
            <xsl:element name="mif:generalization" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of select="."/>
                </xsl:attribute>
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
          <xsl:for-each select="descendant::mif2:annotations/*/mif2:graphicRepresentation">
            <xsl:element name="mif:annotation" namespace="{$mifNamespace}">
              <xsl:for-each select="parent::*/@graphicLinkId">
                <xsl:attribute name="semanticLinkId">
                  <xsl:value-of select="."/>
                </xsl:attribute>
              </xsl:for-each>
              <xsl:apply-templates mode="convertGraphics" select="@*|node()"/>
            </xsl:element>
          </xsl:for-each>
        </xsl:for-each>
      </xsl:element>
    </xsl:if>
  </xsl:template>
  <xsl:template mode="convertGraphics" match="mif2:graphicRepresentation">
    <xsl:message>Got here 2</xsl:message>
    <xsl:for-each select="parent::*/@graphicLinkId">
      <xsl:attribute name="semanticLinkId">
        <xsl:value-of select="."/>
      </xsl:attribute>
    </xsl:for-each>
    <xsl:apply-templates mode="convertGraphics" select="@*"/>
    <xsl:apply-templates mode="convertGraphics" select="node()"/>
  </xsl:template>
  <xsl:template mode="convertGraphics" match="@*|text()|comment()">
    <xsl:copy-of select="."/>
  </xsl:template>
  <xsl:template mode="convertGraphics" match="*">
    <xsl:element name="{name(.)}" namespace="{$mifNamespace}">
      <xsl:apply-templates mode="convertGraphics" select="@*"/>
      <xsl:apply-templates mode="convertGraphics" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template name="getGUID">
    
  </xsl:template>
  <xsl:template mode="html" match="@*|text()|comment()">
    <xsl:copy-of select="."/>
  </xsl:template>
  <xsl:template mode="html" match="*">
    <xsl:element name="{concat('html:', local-name(.))}" namespace="{$htmlNamespace}">
      <xsl:apply-templates mode="html" select="@*"/>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:constructedElement|mif:figureRef|mif:tableRef|mif:itemName|mif:annotationRef|
                                  mif:artifactGroupRef|mif:packageRef|mif:domainAnalysisModelRef|mif:domainInstanceExampleRef|
                                  mif:glossaryRef|mif:glossaryTermRef|mif:storyboardRef|mif:freehandDocumentRef|mif:publicationRef|
                                  mif:datatypeModelRef|mif:datatypeRef|mif:propertyRef|mif:staticModelRef|mif:subjectAreaRef|
                                  mif:classRef|mif:stateRef|mif:transitionRef|mif:attributeRef|mif:associationEndRef|
                                  mif:triggerEventRef|mif:applicationRoleRef|mif:interactionRef|mif:vocabularyModelRef|
                                  mif:vocabularyCodeSystemRef|mif:vocabularyCodeRef|
                                  mif:vocabularyValueSetRef|mif:testScenarioRef|mif:testCaseRef|mif:footnote|mif:externalSpecRef">
    <xsl:element name="{concat($htmlPrefix, 'object')}" namespace="{$htmlNamespace}">
      <xsl:attribute name="name">
        <xsl:value-of select="name(.)"/>
      </xsl:attribute>
      <xsl:for-each select="@*">
        <xsl:element name="{concat($htmlPrefix, 'param')}" namespace="{$htmlNamespace}">
          <xsl:attribute name="name">
            <xsl:choose>
              <xsl:when test="name(.)='realm'">realmNamespace</xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="name(.)"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="value">
            <xsl:value-of select="."/>
          </xsl:attribute>
        </xsl:element>
      </xsl:for-each>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:vocabularyDomainRef">
    <xsl:element name="{concat($htmlPrefix, 'object')}" namespace="{$htmlNamespace}">
	  <xsl:choose>
		<xsl:when test="$schemaVersion='2.1.2'">
		    <xsl:attribute name="name">vocabularyDomainRef</xsl:attribute>
		</xsl:when>
		<xsl:otherwise>
		    <xsl:attribute name="name">conceptDomainRef</xsl:attribute>
		</xsl:otherwise>
	  </xsl:choose>
      <xsl:for-each select="@*">
        <xsl:element name="{concat($htmlPrefix, 'param')}" namespace="{$htmlNamespace}">
          <xsl:attribute name="name">
            <xsl:choose>
              <xsl:when test="name(.)='realm'">realmNamespace</xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="name(.)"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="value">
            <xsl:value-of select="."/>
          </xsl:attribute>
        </xsl:element>
      </xsl:for-each>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:div">
    <xsl:element name="{concat($htmlPrefix, local-name(.))}" namespace="{$htmlNamespace}">
      <xsl:apply-templates mode="html" select="@title"/>
      <xsl:if test="@id">
        <xsl:attribute name="hl7Id">
          <xsl:value-of select="."/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="@numberSectionInd='false'">
        <xsl:attribute name="style">NonNumbered</xsl:attribute>
      </xsl:if>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:table">
    <xsl:element name="{concat($htmlPrefix, local-name(.))}" namespace="{$htmlNamespace}">
      <xsl:if test="@id">
        <xsl:attribute name="hl7Id">
          <xsl:value-of select="@id"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:if test="@borders">
        <xsl:attribute name="border">
          <xsl:value-of select="@borders"/>
        </xsl:attribute>
      </xsl:if>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="@width|@height" priority="5">
    <xsl:if test="number(.) &lt; 1.0">
      <xsl:attribute name="{name(.)}">
        <xsl:value-of select="concat(round(. * 100), '%')"/>
      </xsl:attribute>
    </xsl:if>
  </xsl:template>
  <xsl:template mode="html" match="mif:figure">
    <xsl:element name="{concat($htmlPrefix, 'img')}" namespace="{$htmlNamespace}">
      <xsl:if test="@id">
        <xsl:attribute name="hl7Id">
          <xsl:value-of select="."/>
        </xsl:attribute>
      </xsl:if>
      <xsl:attribute name="alt">
        <xsl:value-of select="mif:caption"/>
      </xsl:attribute>
      
      <xsl:if test="@borders">
        <xsl:attribute name="border">
          <xsl:value-of select="."/>
        </xsl:attribute>
      </xsl:if>
      <xsl:call-template name="handleFigure">
        <xsl:with-param name="namespace" select="$htmlNamespace"/>
      </xsl:call-template>
    </xsl:element>
    <xsl:for-each select="mif:caption">
      <xsl:element name="{concat($htmlPrefix, 'span')}" namespace="{$htmlNamespace}">
        <xsl:apply-templates mode="html" select="node()"/>
      </xsl:element>
    </xsl:for-each>
  </xsl:template>
  <xsl:template mode="html" match="mif:style">
    <xsl:element name="{concat($htmlPrefix, 'span')}" namespace="{$htmlNamespace}">
      <xsl:apply-templates mode="html" select="@*"/>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:externalRef">
    <xsl:element name="{concat($htmlPrefix, 'a')}" namespace="{$htmlNamespace}">
      <xsl:attribute name="href">
        <xsl:value-of select="@ref"/>
      </xsl:attribute>
      <xsl:value-of select="@alt"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:divRef">
    <xsl:element name="{concat($htmlPrefix, 'a')}" namespace="{$htmlNamespace}">
      <xsl:attribute name="href">
        <xsl:value-of select="concat('#div_', @id)"/>
      </xsl:attribute>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:storyboardModelRef">
    <xsl:message>Storyboard models have never existed and aren't supported in MIF2.</xsl:message>
  </xsl:template>
  <xsl:template mode="html" match="mif:note">
    <xsl:element name="{concat($htmlPrefix, 'span')}" namespace="{$htmlNamespace}">
      <xsl:attribute name="style">Note</xsl:attribute>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:indent">
    <xsl:element name="{concat($htmlPrefix, 'span')}" namespace="{$htmlNamespace}">
      <xsl:attribute name="style">Indent</xsl:attribute>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:codeSystemRef">
    <xsl:element name="{concat($htmlPrefix, 'vocabularyCodeSystemRef')}" namespace="{$htmlNamespace}">
      <xsl:apply-templates mode="html" select="@*"/>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:templateRef">
    <xsl:element name="{concat($htmlPrefix, 'staticModelRef')}" namespace="{$htmlNamespace}">
      <xsl:apply-templates mode="html" select="@*"/>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:itsRef">
    <xsl:element name="{concat($htmlPrefix, 'freehandDocumentRef')}" namespace="{$htmlNamespace}">
      <xsl:apply-templates mode="html" select="@*"/>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template mode="html" match="mif:communicationProtocolRef">
    <xsl:element name="{concat($htmlPrefix, 'freehandDocumentRef')}" namespace="{$htmlNamespace}">
      <xsl:apply-templates mode="html" select="@*"/>
      <xsl:apply-templates mode="html" select="node()"/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="mif:p[mif:p]" priority="5">
    <xsl:apply-templates mode="html" select="node()"/>
  </xsl:template>
  <xsl:template mode="findBaseClassName" match="mif:class">
    <xsl:variable name="ancestorName">
      <xsl:apply-templates mode="findBaseClassName" select="//mif:class[not(@name='InfrastructureRoot') and mif:specializationChild/@childClassName=current()/@name]"/>
    </xsl:variable>
    <xsl:value-of select="$ancestorName"/>
    <xsl:if test="$ancestorName=''">
      <xsl:value-of select="@name"/>
    </xsl:if>
  </xsl:template>
  
</xsl:stylesheet>
