import { Box, Typography } from "@mui/material"

type ScraperResultTextProps = {
    url: string
    keyword: string
    result: string
    matches: number
    error: boolean
}

export const ScraperResultText = ({
    url,
    keyword,
    result,
    matches,
    error,
}: ScraperResultTextProps) => {
    return (
        <Box>
            <Box
                display={"flex"}
                flexDirection={"column"}
                justifyContent={"center"}
                alignItems={"center"}
            >
                {error ? (
                    <Box width={"600px"} py={3}>
                        <Typography
                            variant="h4"
                            fontWeight={600}
                            color={"red"}
                            textAlign={"center"}
                        >
                            Error! There was an error scraping that url, please
                            try a different url.
                        </Typography>
                    </Box>
                ) : (
                    <>
                        <Typography
                            variant="h4"
                            color={matches === 0 || error ? "red" : "green"}
                            fontWeight={600}
                            py={3}
                            textAlign={"center"}
                        >
                            {matches}{" "}
                            {matches === 1 ? "occurrence" : "occurrences"}{" "}
                            found!
                        </Typography>
                        {matches !== 0 ? (
                            <Box width={"800px"}>
                                <Typography textAlign={"center"}>
                                    "{keyword}" was found in search results on "
                                    {url}": {matches}{" "}
                                    {matches === 1 ? "time" : "times"} at result{" "}
                                    {matches === 1 ? "number" : "numbers"}:{" "}
                                    {result}
                                </Typography>
                            </Box>
                        ) : (
                            <Box width={"800px"}>
                                <Typography textAlign={"center"}>
                                    "{keyword}" was found in "{url}": 0 times.
                                    Try another url/keyword!
                                </Typography>
                            </Box>
                        )}
                    </>
                )}
            </Box>
        </Box>
    )
}
