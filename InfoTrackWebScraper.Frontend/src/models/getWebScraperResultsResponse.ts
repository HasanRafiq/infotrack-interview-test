import { OccurrenceInfoDto } from "./occurrenceInfoDto";

export interface GetWebScraperResultsResponse {
    occurrenceInfoDto: OccurrenceInfoDto
    error?: boolean
}