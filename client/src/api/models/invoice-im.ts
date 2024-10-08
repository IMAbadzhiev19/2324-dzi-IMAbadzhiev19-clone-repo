/* tslint:disable */
/* eslint-disable */
/**
 * PMS.Api
 * Pharmacy Management System API
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { MedicineInvoiceModel } from './medicine-invoice-model';
/**
 * 
 * @export
 * @interface InvoiceIM
 */
export interface InvoiceIM {
    /**
     * 
     * @type {number}
     * @memberof InvoiceIM
     */
    totalPrice: number;
    /**
     * 
     * @type {string}
     * @memberof InvoiceIM
     */
    pharmacistId: string;
    /**
     * 
     * @type {string}
     * @memberof InvoiceIM
     */
    depotId: string;
    /**
     * 
     * @type {string}
     * @memberof InvoiceIM
     */
    pharmacyId: string;
    /**
     * 
     * @type {Array<MedicineInvoiceModel>}
     * @memberof InvoiceIM
     */
    medicines?: Array<MedicineInvoiceModel> | null;
}
